using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.NPC.Robot.States;
using LastBreakthrought.Player;
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
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        private RobotBattary _battary;

        private NPCStateMachine _stateMachine;
        private PlayerHandler _playerHandler;
        private ICoroutineRunner _coroutineRunner;
        private IConfigProviderService _configProvider;
        private List<RobotChargingPlace> _chargingPlaces;

        [Inject]
        private void Construct(PlayerHandler playerHandler, ICoroutineRunner coroutineRunner, IConfigProviderService configProviderService)
        {
            _playerHandler = playerHandler;
            _coroutineRunner = coroutineRunner;
            _configProvider = configProviderService;
        }

        public void OnCreated(BoxCollider wanderingZone, List<RobotChargingPlace> chargingPlaces)
        {
            _stateMachine = new NPCStateMachine();
            _battary = new RobotBattary(10f);
            _chargingPlaces = chargingPlaces;

            var wandering = new RobotWanderingState(this, _coroutineRunner, _agent, _animator, wanderingZone, _battary, 1.5f);
            var followingPlayer = new RobotFollowingPlayerState(this, _coroutineRunner, _agent, _playerHandler, _animator, _battary, 1.5f);
            var recharging = new RobotRechargingState(this, _coroutineRunner, _agent, _playerHandler, _animator, _battary, 1.5f);

            _stateMachine.AddTransition(wandering, recharging, () => _battary.NeedToBeRecharged);
            _stateMachine.AddTransition(recharging, wandering, () => !_battary.NeedToBeRecharged);

            _stateMachine.EnterInState(wandering);
        }

        private void Update()
        {
            _stateMachine.Tick();
            Debug.Log($"{_battary.Capacity}");
        }

        public void ApplyDamage(float damage)
        {

        }

        public RobotChargingPlace GetAvelableCharingPlace()
        {
            foreach (var chargingPlace in _chargingPlaces)
                if (!chargingPlace.IsOccupiad)
                    return chargingPlace;

            return null;
        }
    }

    public class RobotBattary
    {
        private const float INCREASE_CAPACITY_DELTA = 2f;
        private const float DECREASE_CAPACITY_DELTA = 1f;
        private const float CAPACITY_LIMIT = 3f;

        private float _maxCapacity;
        private float _currentCapacity;
        public float Capacity 
        { 
            get => _currentCapacity; 
            private set 
            { 
                _currentCapacity = value;
                
                if (_currentCapacity > _maxCapacity)
                    _currentCapacity = _maxCapacity;

                if (_currentCapacity < 0)
                    _currentCapacity = 0;
            } 
        }
        public bool NeedToBeRecharged { get; set; }

        public RobotBattary(float maxCapacity)
        {
            _maxCapacity = maxCapacity;
            _currentCapacity = _maxCapacity;
            NeedToBeRecharged = false;
        }

        public void CheckIfCapacityIsRechedLimit()
        {
            if (Capacity <= CAPACITY_LIMIT)
                NeedToBeRecharged = true;
        }
        public bool IsCapacityFull()
        {
            if (Capacity == _maxCapacity)
                return true;
            return false;
        }

        public void DecreaseCapacity() => 
            Capacity -= DECREASE_CAPACITY_DELTA * Time.deltaTime;

        public void IncreaseCapacity() => 
            Capacity += INCREASE_CAPACITY_DELTA * Time.deltaTime;
    }
}
