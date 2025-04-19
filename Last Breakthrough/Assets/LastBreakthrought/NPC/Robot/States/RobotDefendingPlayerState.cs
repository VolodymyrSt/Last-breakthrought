using LastBreakthrought.NPC.Base;
using LastBreakthrought.NPC.Enemy;
using LastBreakthrought.Player;
using LastBreakthrought.Util;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotDefendingPlayerState : INPCState
    {
        private const string IS_MOVING = "isMoving";
        private const string IS_Defending = "isDefending";
        private const float STOP_DISTANCE = 2.5f;
        private const float ATTACK_COOLDOWN = 2f;

        private readonly RobotDefender _robot;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly RobotBattary _robotBattary;
        private readonly float _followingSpeed;

        private Coroutine _defendingCoroutine;

        public RobotDefendingPlayerState(RobotDefender robot, ICoroutineRunner coroutineRunner, NavMeshAgent agent,  Animator animator,
            RobotBattary robotBattary, float followingSpeed)
        {
            _agent = agent;
            _robot = robot;
            _coroutineRunner = coroutineRunner;
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
            _defendingCoroutine = _coroutineRunner.PerformCoroutine(PerformDefending());
        }

        public void Exit()
        {
            _animator.SetBool(IS_MOVING, false);
            _coroutineRunner.HandleStopCoroutine(_defendingCoroutine);
        }

        public void Update()
        {
            _robotBattary.DecreaseCapacity();
            _robotBattary.CheckIfCapacityIsRechedLimit();
        }

        private IEnumerator PerformDefending()
        {
            while (true)
            {
                if (_robot.Target == null) yield return null;

                _agent.SetDestination(_robot.Target.GetPosition());

                if (_agent.remainingDistance < STOP_DISTANCE + 0.01f)
                {
                    PerformAttack();
                    yield return new WaitForSecondsRealtime(ATTACK_COOLDOWN);

                    if (CheckIfTargetDied())
                        yield break;
                }
                else
                    ResetAnimation();

                yield return null;
            }
        }

        private bool CheckIfTargetDied()
        {
            if (_robot.Target.IsDied())
            {
                _robot.ClearTarget();
                return true;
            }
            return false;
        }

        private void PerformAttack()
        {
            var enemy = _robot.Target as EnemyBase;
            enemy?.ApplyDamage(35f);

            _animator.SetBool(IS_Defending, true);
            _animator.SetBool(IS_MOVING, false);
        }
        private void ResetAnimation()
        {
            _animator.SetBool(IS_MOVING, true);
            _animator.SetBool(IS_Defending, false);
        }
    }
}
