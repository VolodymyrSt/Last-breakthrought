using Assets.LastBreakthrought.UI.Massage;
using LastBreakthrought.Configs.Robot;
using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.NPC.Robot.States;
using LastBreakthrought.Player;
using LastBreakthrought.Util;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace LastBreakthrought.NPC.Robot
{
    public class RobotBase : MonoBehaviour, IRobot, IDamagable
    {
        [Header("Base:")]
        [SerializeField] protected NavMeshAgent Agent;
        [SerializeField] protected Animator Animator;

        protected RobotBattary Battary;
        protected NPCStateMachine StateMachine;
        protected PlayerHandler PlayerHandler;
        protected ICoroutineRunner CoroutineRunner;
        protected RobotConfigSO RobotData;
        protected IEventBus EventBus;
        protected MassageHandler MassageHandler;

        protected RobotWanderingState RobotWanderingState;
        protected RobotFollowingPlayerState RobotFollowingPlayerState;
        protected RobotRechargingState RobotRechargingState;

        private IConfigProviderService _configProvider;
        private List<RobotChargingPlace> _chargingPlaces;

        protected bool IsWanderingState { get; set; }
        protected bool IsFollowingState { get; set; }

        [Inject]
        private void Construct(PlayerHandler playerHandler, ICoroutineRunner coroutineRunner, IConfigProviderService configProviderService,
            IEventBus eventBus, MassageHandler massageHandler)
        {
            PlayerHandler = playerHandler;
            CoroutineRunner = coroutineRunner;
            EventBus = eventBus;
            _configProvider = configProviderService;
            MassageHandler = massageHandler;
        }

        public virtual void OnCreated(BoxCollider wanderingZone, List<RobotChargingPlace> chargingPlaces, string id)
        {
            _chargingPlaces = chargingPlaces;
            RobotData = _configProvider.RobotConfigHolderSO.GetRobotDataById(id);

            Battary = new RobotBattary(RobotData.MaxBattaryCapacity, RobotData.CapacityLimit);
            StateMachine = new NPCStateMachine();

            RobotWanderingState = new RobotWanderingState(CoroutineRunner, Agent, Animator, wanderingZone, Battary, RobotData.WandaringSpeed);
            RobotFollowingPlayerState = new RobotFollowingPlayerState(Agent, PlayerHandler, Animator, Battary, RobotData.GeneralSpeed);
            RobotRechargingState = new RobotRechargingState(this, Agent, Animator, Battary, RobotData.GeneralSpeed);
        }

        private void Update() => StateMachine.Tick();

        public void ApplyDamage(float damage){}

        public void SetFollowingPlayerState()
        {
            if (Battary.NeedToBeRecharged)
            {
                MassageHandler.ShowMassage("Robot battary is too low");
                return;
            }

            IsFollowingState = true;
            IsWanderingState = false;
        }

        public void SetWanderingState()
        {
            if (Battary.NeedToBeRecharged)
            {
                MassageHandler.ShowMassage("Robot battary is too low");
                return;
            }

            IsWanderingState = true;
            IsFollowingState = false;
        }

        public virtual void DoWork() { }

        public RobotConfigSO GetRobotData() => RobotData;

        public RobotBattary GetRobotBattary() => Battary;

        public RobotChargingPlace FindAvelableCharingPlace()
        {
            foreach (var chargingPlace in _chargingPlaces)
                if (!chargingPlace.IsOccupiad)
                    return chargingPlace;

            return null;
        }

        public void DoNothing() { }
    }
}
