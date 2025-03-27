using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.Player;
using LastBreakthrought.Util;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotRechargingState : INPCState
    {
        private const string IS_RIDING = "isRiding";
        private const float STOP_DISTANCE = 1f;

        private readonly RobotBase _robot;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly PlayerHandler _playerHandler;
        private readonly Animator _animator;
        private readonly RobotBattary _robotBattary;

        private RobotChargingPlace _chargingPlace;
        private readonly float _followingSpeed;

        private bool _isRechedChargingPlace = false;

        public RobotRechargingState(RobotBase robot, ICoroutineRunner coroutineRunner, NavMeshAgent agent,
            PlayerHandler playerHandler, Animator animator, RobotBattary robotBattary, float followingSpeed)
        {
            _robot = robot;
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _playerHandler = playerHandler;
            _animator = animator;
            _robotBattary = robotBattary;
            _followingSpeed = followingSpeed;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _agent.speed = _followingSpeed;
            _agent.stoppingDistance = STOP_DISTANCE;
            _animator.SetBool(IS_RIDING, true);

            _chargingPlace = _robot.GetAvelableCharingPlace();
            _chargingPlace.IsOccupiad = true;
        }

        public void Exit() => 
            _animator.SetBool(IS_RIDING, false);

        public void Update()
        {
            if (!_isRechedChargingPlace)
            {
                _agent.SetDestination(_chargingPlace.GetChargingPosition());

                if (_agent.remainingDistance <= STOP_DISTANCE && !_agent.pathPending)
                    _isRechedChargingPlace = true;
            }
            else
            {
                _robotBattary.IncreaseCapacity();
                if (_robotBattary.IsCapacityFull())
                {
                    _robotBattary.NeedToBeRecharged = false;
                    _chargingPlace.IsOccupiad = false;
                    _isRechedChargingPlace = false;
                }
            }
        }
    }
}
