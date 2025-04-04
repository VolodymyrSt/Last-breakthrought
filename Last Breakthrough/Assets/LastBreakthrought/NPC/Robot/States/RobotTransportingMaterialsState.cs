using LastBreakthrought.Logic.MaterialRecycler;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotTransportingMaterialsState : INPCState
    {
        private const string IS_Moving = "isMoving";
        private const string IS_TRANSPORTING = "isTransporting";
        private const float TRANSPORTING_TIME = 4f;
        private const float STOP_DISTANCE = 2f;

        private readonly RobotTransporter _robot;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly RobotBattary _robotBattary;
        private readonly RecycleMachine _recycleMachine;
        private readonly float _movingSpeed;

        private Coroutine _transportingCoroutine;
        private bool _isCarring = false;

        public RobotTransportingMaterialsState(RobotTransporter robot, ICoroutineRunner coroutineRunner, NavMeshAgent agent,
            Animator animator, RobotBattary robotBattary, RecycleMachine recycleMachine, float followingSpeed)
        {
            _robot = robot;
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _animator = animator;
            _robotBattary = robotBattary;
            _recycleMachine = recycleMachine;
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
            _animator.SetBool(IS_TRANSPORTING, false);
            _animator.SetBool(IS_Moving, false);

            _isCarring = false;

            if (_transportingCoroutine != null)
                _coroutineRunner.HandleStopCoroutine(_transportingCoroutine);
        }

        public void Update()
        {
            _agent.SetDestination(_recycleMachine.GetMachinePosition());

            var isArrived = Vector3.Distance(_agent.transform.position, _recycleMachine.GetMachinePosition()) <= 0.1f + STOP_DISTANCE;

            if (isArrived && !_isCarring)
            {
                _isCarring = true;
                _animator.SetBool(IS_Moving, false);
                _transportingCoroutine = _coroutineRunner.PerformCoroutine(StartTransporting());
            }

            _robotBattary.DecreaseCapacity();
            _robotBattary.CheckIfCapacityIsRechedLimit();
        }

        private IEnumerator StartTransporting()
        {
            while (_robot.TransportedMaterials.Count > 0)
            {
                _animator.SetBool(IS_TRANSPORTING, true);

                for (int i = 0; i < _robot.TransportedMaterials.Count; i++)
                {
                    var transportedMaterial = _robot.TransportedMaterials[i];

                    if (transportedMaterial != null)
                    {
                        yield return new WaitForSecondsRealtime(TRANSPORTING_TIME);
                        _recycleMachine.RecycleEntireMaterial(transportedMaterial);
                        _robot.TransportedMaterials.Remove(transportedMaterial);
                    }
                }
            }

            _robot.ClearCrashedShip();
            _robot.HasLoadedMaterials = false;
        }
    }
}
