using LastBreakthrought.Configs.Robot;
using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.Massage;
using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.Logic.InteractionZone;
using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.NPC.Robot.States;
using LastBreakthrought.Player;
using LastBreakthrought.UI.Inventory;
using LastBreakthrought.Util;
using System.Collections.Generic;
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
        [SerializeField] private InteractionZoneHandler _zoneHandler;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private RequireDetailsForCreating _requireDetailsToRepair;

        protected RobotBattary Battary;
        protected RobotHealth Health;
        protected NPCStateMachine StateMachine;
        protected PlayerHandler PlayerHandler;
        protected ICoroutineRunner CoroutineRunner;
        protected RobotConfigSO RobotData;
        protected IEventBus EventBus;
        protected IMassageHandlerService MassageHandler;

        protected RobotWanderingState RobotWanderingState;
        protected RobotFollowingPlayerState RobotFollowingPlayerState;
        protected RobotRechargingState RobotRechargingState;
        protected RobotDestroyedState RobotDestroyedState;

        private IConfigProviderService _configProvider;
        private DetailsContainer _detailsContainer;
        private DetailInventoryMenuPanelHandler _detailInventory;
        private List<RobotChargingPlace> _chargingPlaces;

        protected bool IsWanderingState { get; set; }
        protected bool IsFollowingState { get; set; }
        protected bool IsRobotDestroyed { get; set; }

        [Inject]
        private void Construct(PlayerHandler playerHandler, ICoroutineRunner coroutineRunner, IConfigProviderService configProviderService,
            IEventBus eventBus, IMassageHandlerService massageHandler, DetailsContainer detailsContainer, DetailInventoryMenuPanelHandler detailInventory)
        {
            PlayerHandler = playerHandler;
            CoroutineRunner = coroutineRunner;
            EventBus = eventBus;
            _configProvider = configProviderService;
            MassageHandler = massageHandler;
            _detailsContainer = detailsContainer;
            _detailInventory = detailInventory;
        }

        public virtual void OnCreated(BoxCollider wanderingZone, List<RobotChargingPlace> chargingPlaces, string id)
        {
            _chargingPlaces = chargingPlaces;
            RobotData = _configProvider.RobotConfigHolderSO.GetRobotDataById(id);

            Battary = new RobotBattary(RobotData.MaxBattaryCapacity, RobotData.CapacityLimit);
            Health = new RobotHealth(RobotData.MaxHealth);
            StateMachine = new NPCStateMachine();

            RobotWanderingState = new RobotWanderingState(CoroutineRunner, Agent, Animator, wanderingZone, Battary, RobotData.WandaringSpeed);
            RobotFollowingPlayerState = new RobotFollowingPlayerState(Agent, PlayerHandler, Animator, Battary, RobotData.GeneralSpeed);
            RobotRechargingState = new RobotRechargingState(this, Agent, Animator, Battary, RobotData.GeneralSpeed);
            RobotDestroyedState = new RobotDestroyedState(Agent, Animator, _collider, _zoneHandler);

            StateMachine.AddTransition(RobotWanderingState, RobotDestroyedState, () => IsRobotDestroyed);
            StateMachine.AddTransition(RobotFollowingPlayerState, RobotDestroyedState, () => IsRobotDestroyed);
            StateMachine.AddTransition(RobotRechargingState, RobotDestroyedState, () => IsRobotDestroyed);

            StateMachine.AddTransition(RobotDestroyedState, RobotWanderingState, () => !IsRobotDestroyed && IsWanderingState);
            StateMachine.AddTransition(RobotDestroyedState, RobotFollowingPlayerState, () => !IsRobotDestroyed && IsFollowingState);
            StateMachine.AddTransition(RobotDestroyedState, RobotRechargingState, () => !IsRobotDestroyed && Battary.NeedToBeRecharged);
            StateMachine.AddTransition(RobotDestroyedState, RobotWanderingState, () => !IsRobotDestroyed && !IsWanderingState && !IsFollowingState);

            _zoneHandler.Disactivate();
        }

        private void Update() => StateMachine.Tick();

        public virtual void ApplyDamage(float damage)
        {
            Health.TakeDamage(damage);

            if (Health.IsHealthGone())
                IsRobotDestroyed = true;
        }

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

        public RobotHealth GetRobotHealth() => Health;

        public List<ShipDetailEntity> GetRequiredDetailsToRepair() => 
            _requireDetailsToRepair.GetRequiredShipDetails();

        public RobotChargingPlace FindAvelableCharingPlace()
        {
            foreach (var chargingPlace in _chargingPlaces)
                if (!chargingPlace.IsOccupiad)
                    return chargingPlace;

            return null;
        }

        public void TryToRepair()
        {
            if (_detailsContainer.IsSearchedDetailsAllFound(GetRequiredDetailsToRepair()))
            {
                IsRobotDestroyed = false;
                Health.FullRecover();
                _detailsContainer.GiveDetails(GetRequiredDetailsToRepair());
                _detailInventory.UpdateInventoryDetails(GetRequiredDetailsToRepair());
            }
            else
                MassageHandler.ShowMassage("You don`t have the right details");
        }
    }
}
