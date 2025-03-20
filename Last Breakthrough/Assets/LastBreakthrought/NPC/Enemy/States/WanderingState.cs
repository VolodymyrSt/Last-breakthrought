using LastBreakthrought.NPC.Base;
using LastBreakthrought.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Enemy
{
    public class WanderingState : INPCState
    {
        private const string IS_WALKING = "isWalking";
        private const float NAVMESH_SAMPLE_RANGE = 2f;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly BoxCollider _wanderingZone;

        private Coroutine _wanderingCoroutine;

        private float _minWaitTime = 1f;
        private float _maxWaitTime = 3f;
        private float _movementTimeout = 5f;
        private float _wanderingSpeed = 1.5f;

        public WanderingState(ICoroutineRunner coroutineRunner, NavMeshAgent agent, BoxCollider wanderingZone, Animator animator)
        {
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _wanderingZone = wanderingZone;
            _animator = animator;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _agent.speed = _wanderingSpeed;
            _wanderingCoroutine = _coroutineRunner.PerformCoroutine(StartWandering());
        }

        private IEnumerator StartWandering()
        {
            while (true)
            {
                Vector3 destination = GetRandomPositionForNavMesh();
                if (destination != Vector3.negativeInfinity)
                {
                    _agent.SetDestination(destination);
                    SetWalkingAnimation(true);

                    yield return WaitForDestinationReached();
                    SetWalkingAnimation(false);
                }

                yield return WaitBeforeNext();
            }
        }


        private IEnumerator WaitForDestinationReached()
        {
            float elapsedTime = 0f;
            while (elapsedTime < _movementTimeout)
            {
                if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                        yield break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _agent.ResetPath();
        }

        private Vector3 GetRandomPositionForNavMesh()
        {
            Vector3 randomPoint = GetRandomPointInZone();

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, NAVMESH_SAMPLE_RANGE, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return Vector3.negativeInfinity;
        }

        private Vector3 GetRandomPointInZone()
        {
            Vector3 localPosition = new Vector3(
                Random.Range(-_wanderingZone.size.x / 2, _wanderingZone.size.x / 2),
                0, 
                Random.Range(-_wanderingZone.size.z / 2, _wanderingZone.size.z / 2)
            );

            localPosition += _wanderingZone.center;
            return _wanderingZone.transform.TransformPoint(localPosition);
        }

        private IEnumerator WaitBeforeNext()
        {
            float waitTime = Random.Range(_minWaitTime, _maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }

        private void SetWalkingAnimation(bool isMoving) => 
            _animator.SetBool(IS_WALKING, isMoving);

        public void Update(){ }
        public void Exit() 
        {
            if (_wanderingCoroutine != null)
                _coroutineRunner.HandleStopCoroutine(_wanderingCoroutine);

            _agent.ResetPath();
        }
    }
}
