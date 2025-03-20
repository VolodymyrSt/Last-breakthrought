using LastBreakthrought.NPC.Base;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Enemy
{
    public class ChassingState : INPCState
    {
        private NavMeshAgent _agent;
        private Transform _target;
        private Animator _animator;

        public ChassingState(NavMeshAgent agent, Transform target, Animator animator)
        {
            _agent = agent;
            _target = target;
            _animator = animator;
        }

        public void Enter()
        {
            _agent.SetDestination(_target.position);
        }

        public void Update()
        {
            
        }

        public void Exit(){ }
    }
}
