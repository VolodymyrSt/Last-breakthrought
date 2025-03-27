using LastBreakthrought.NPC.Base;
using LastBreakthrought.Player;
using LastBreakthrought.Util;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotFollowingPlayerState : INPCState
    {
        private const string IS_RIDING = "isRiding";
        private const float STOP_DISTANCE = 2.5f;

        private readonly RobotBase _robot;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly PlayerHandler _playerHandler;
        private readonly Animator _animator;
        private readonly RobotBattary _robotBattary;
        private readonly float _followingSpeed;

        public RobotFollowingPlayerState(RobotBase robot, ICoroutineRunner coroutineRunner, NavMeshAgent agent,
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
        }

        public void Exit() => 
            _animator.SetBool(IS_RIDING, false);

        public void Update()
        {
            _agent.SetDestination(_playerHandler.GetPosition());

            _robotBattary.DecreaseCapacity();
            _robotBattary.CheckIfCapacityIsRechedLimit();
        }
    }
}
