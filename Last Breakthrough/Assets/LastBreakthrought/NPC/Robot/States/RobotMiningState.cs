using LastBreakthrought.CrashedShip;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotMiningState : INPCState
    {
        private const string IS_RIDING = "isRiding";
        private const string IS_MINING = "isMining";
        private const float MINING_TIME = 4f;
        private const float STOP_DISTANCE = 4f;

        private readonly RobotBase _robot;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly ICrashedShip _crashedShip;
        private readonly Animator _animator;
        private readonly float _followingSpeed;

        private Coroutine _miningCoroutine;

        public RobotMiningState(RobotBase robot, ICoroutineRunner coroutineRunner, NavMeshAgent agent,
            Animator animator, ICrashedShip crashedShip, float followingSpeed)
        {
            _robot = robot;
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _animator = animator;
            _crashedShip = crashedShip;
            _followingSpeed = followingSpeed;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _agent.speed = _followingSpeed;
            _agent.stoppingDistance = STOP_DISTANCE;
            _animator.SetBool(IS_RIDING, true);
        }

        public void Exit()
        {
            _animator.SetBool(IS_MINING, false);
            _animator.SetBool(IS_RIDING, false);
        }

        public void Update()
        {
            if (_crashedShip == null) return;

            _agent.SetDestination(_crashedShip.GetPosition());

            if (_agent.remainingDistance <= 0.1f + STOP_DISTANCE)
            {
                _coroutineRunner.PerformCoroutine(StartMining());
            }        
        }

        private IEnumerator StartMining()
        {
            while (true) //inventory.space < 0 || _crashedShip.Materials > 0
            {
                if (_crashedShip == null) yield break;

                _animator.SetBool(IS_RIDING, false);
                _animator.SetBool(IS_MINING, true);

                yield return new WaitForSecondsRealtime(MINING_TIME);
                _crashedShip.MineEntireMaterial();
            }
        }
    }
}
