using LastBreakthrought.NPC.Base;
using LastBreakthrought.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotMiningState : INPCState
    {
        private const string IS_Moving = "isMoving";
        private const string IS_MINING = "isMining";
        private const float MINING_TIME = 4f;
        private const float STOP_DISTANCE = 4f;

        private readonly RobotMiner _robot;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly RobotBattary _robotBattary;
        private readonly float _movingSpeed;

        private Coroutine _miningCoroutine;
        private bool _isMining = false;

        public RobotMiningState(RobotMiner robot, ICoroutineRunner coroutineRunner, NavMeshAgent agent,
            Animator animator, RobotBattary robotBattary, float followingSpeed)
        {
            _robot = robot;
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _animator = animator;
            _robotBattary = robotBattary;
            _movingSpeed = followingSpeed;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _agent.speed = _movingSpeed;
            _agent.stoppingDistance = STOP_DISTANCE;
            _animator.SetBool(IS_Moving, true);
        }

        public void Exit()
        {
            _animator.SetBool(IS_MINING, false);
            _animator.SetBool(IS_Moving, false);

            _isMining = false;

            if (_miningCoroutine != null)
                _coroutineRunner.HandleStopCoroutine(_miningCoroutine);
        }

        public void Update()
        {
            if (_robot.CrashedShip == null) return;

            _agent.SetDestination(_robot.CrashedShip.GetPosition());

            var isArrived = Vector3.Distance(_agent.transform.position, _robot.CrashedShip.GetPosition()) <= 0.1f + STOP_DISTANCE;

            if (isArrived && !_isMining)
            {
                _isMining = true;
                _animator.SetBool(IS_Moving, false);
                _miningCoroutine = _coroutineRunner.PerformCoroutine(StartMining());
            }

            _robotBattary.DecreaseCapacity();
            _robotBattary.CheckIfCapacityIsRechedLimit();
        }

        private IEnumerator StartMining()
        {
            if (_robot.CrashedShip == null) yield break;
            
            while (_robot.CrashedShip.Materials.Count > 0)
            {
                _animator.SetBool(IS_MINING, true);

                yield return new WaitForSecondsRealtime(MINING_TIME);
                _robot.CrashedShip.MineEntireMaterial();
            }
            _robot.ClearCrashedShip();
        }
    }
}
