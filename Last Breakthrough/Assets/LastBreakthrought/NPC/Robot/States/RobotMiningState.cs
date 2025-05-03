using LastBreakthrought.Infrustructure.Services.AudioService;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
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
        private readonly IEventBus _eventBus;
        private readonly IAudioService _audioService;
        private readonly float _movingSpeed;

        private Coroutine _miningCoroutine;
        private bool _isMining = false;

        public RobotMiningState(RobotMiner robot, ICoroutineRunner coroutineRunner
            , NavMeshAgent agent, Animator animator, RobotBattary robotBattary, IEventBus eventBus
            , IAudioService audioService, float followingSpeed)
        {
            _robot = robot;
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _animator = animator;
            _robotBattary = robotBattary;
            _eventBus = eventBus;
            _audioService = audioService;
            _movingSpeed = followingSpeed;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _agent.speed = _movingSpeed;
            _agent.stoppingDistance = STOP_DISTANCE;
            _animator.SetBool(IS_Moving, true);

            _eventBus.SubscribeEvent<OnGamePausedSignal>(StopMining);
            _eventBus.SubscribeEvent<OnGameResumedSignal>(ContinueMining);
        }

        public void Exit()
        {
            _animator.SetBool(IS_MINING, false);
            _animator.SetBool(IS_Moving, false);

            _isMining = false;

            if (_miningCoroutine != null)
                _coroutineRunner.HandleStopCoroutine(_miningCoroutine);

            ClearMiningSound();

            _eventBus.UnSubscribeEvent<OnGamePausedSignal>(StopMining);
            _eventBus.UnSubscribeEvent<OnGameResumedSignal>(ContinueMining);
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

            _animator.SetBool(IS_MINING, true);
            PlayMiningSound();

            while (_robot.CrashedShip.Materials.Count > 0)
            {
                yield return new WaitForSeconds(MINING_TIME);
                _robot.CrashedShip.MineEntireMaterial();
            }
            _robot.ClearCrashedShip();
        }

        private void StopMining(OnGamePausedSignal signal) => 
            _coroutineRunner.HandleStopCoroutine(_miningCoroutine);

        private void ContinueMining(OnGameResumedSignal signal) => 
            _miningCoroutine = _coroutineRunner.PerformCoroutine(StartMining());

        private void PlayMiningSound() =>
            _audioService.PlayOnObject(Configs.Sound.SoundType.MinerMining, _robot, true);

        private void ClearMiningSound() => 
            _audioService.StopOnObject(_robot, Configs.Sound.SoundType.MinerMining);
    }
}
