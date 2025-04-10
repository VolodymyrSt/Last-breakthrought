using LastBreakthrought.Logic.InteractionZone;
using LastBreakthrought.NPC.Base;
using UnityEngine;
using UnityEngine.AI;

namespace LastBreakthrought.NPC.Robot.States
{
    public class RobotDestroyedState : INPCState
    {
        private const string IS_DESTROYED = "isDestroyed";

        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly BoxCollider _boxCollider;
        private readonly InteractionZoneHandler _zoneHandler;

        public RobotDestroyedState(NavMeshAgent agent, Animator animator, BoxCollider boxCollider, InteractionZoneHandler zoneHandler)
        {
            _agent = agent;
            _animator = animator;
            _boxCollider = boxCollider;
            _zoneHandler = zoneHandler;
        }

        public void Enter()
        {
            _agent.enabled = false;
            _boxCollider.enabled = false;
            _animator.SetBool(IS_DESTROYED, true);
            _zoneHandler.Activate();
        }

        public void Exit() 
        {
            _agent.enabled = true;
            _boxCollider.enabled = true;
            _animator.SetBool(IS_DESTROYED, false);
            _zoneHandler.Disactivate();
        }

        public void Update(){}
    }
}
