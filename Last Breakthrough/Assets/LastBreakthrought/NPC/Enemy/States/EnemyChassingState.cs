using LastBreakthrought.NPC.Base;
using LastBreakthrought.Player;
using LastBreakthrought.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Enemy
{
    public class EnemyChassingState : INPCState
    {
        private const string IS_CHASSING = "isWalking";
        private const float CHASSING_TIME_AFTER_WHICH_CHECK_TARGET = 5f;

        private readonly EnemyBase _enemy;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;

        private Coroutine _isTargetEscapeCoroutine;

        private float _chassingSpeed;

        public EnemyChassingState(EnemyBase enemy, ICoroutineRunner  coroutineRunner,NavMeshAgent agent, Animator animator, float chassingSpeed)
        {
            _enemy = enemy;
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _animator = animator;

            _chassingSpeed = chassingSpeed;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _agent.speed = _chassingSpeed;

            _isTargetEscapeCoroutine = _coroutineRunner.PerformCoroutine(CheckIfTargetEscaped());
            SetChassingAnimation(true);
        }
        public void Update()
        {
            _agent.SetDestination(_enemy.Target.GetPosition());
            _enemy.TryToAttackTarget();
        }

        public void Exit()
        {
            if (_isTargetEscapeCoroutine != null)
                _coroutineRunner.HandleStopCoroutine(_isTargetEscapeCoroutine);

            SetChassingAnimation(false);
        }

        private IEnumerator CheckIfTargetEscaped()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(CHASSING_TIME_AFTER_WHICH_CHECK_TARGET);

                _enemy.TryToFindTarget();
            }
        }

        private void SetChassingAnimation(bool isChassing) =>
            _animator.SetBool(IS_CHASSING, isChassing);
    }
}
