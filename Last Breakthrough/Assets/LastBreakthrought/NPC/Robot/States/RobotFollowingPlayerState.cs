using LastBreakthrought.NPC.Base;
using LastBreakthrought.Player;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotFollowingPlayerState : INPCState
    {
        private const string IS_MOVING = "isMoving";
        private const float STOP_DISTANCE = 2.5f;

        private readonly NavMeshAgent _agent;
        private readonly PlayerHandler _playerHandler;
        private readonly Animator _animator;
        private readonly RobotBattary _robotBattary;
        private readonly float _followingSpeed;

        public RobotFollowingPlayerState(NavMeshAgent agent, PlayerHandler playerHandler, Animator animator,
            RobotBattary robotBattary, float followingSpeed)
        {
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
            _animator.SetBool(IS_MOVING, true);
        }

        public void Exit() => 
            _animator.SetBool(IS_MOVING, false);

        public void Update()
        {
            _agent.SetDestination(_playerHandler.GetPosition());

            if (_agent.remainingDistance < STOP_DISTANCE + 0.01f)
                _animator.SetBool(IS_MOVING, false);
            else
                _animator.SetBool(IS_MOVING, true);

            _robotBattary.DecreaseCapacity();
            _robotBattary.CheckIfCapacityIsRechedLimit();
        }
    }
}
