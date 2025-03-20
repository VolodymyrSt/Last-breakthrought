using LastBreakthrought.CrashedShip;
using LastBreakthrought.Logic.ShipMaterial;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.Player;
using LastBreakthrought.Util;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using static UnityEngine.GraphicsBuffer;

namespace LastBreakthrought.NPC.Enemy
{
    public class EnemyController : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        private NPCStateMachine _stateMachine;
        private PlayerHandler _playerHandler;
        private ICoroutineRunner _coroutineRunner;
        private BoxCollider _wanderingZone;

        private Transform _target = null;

        [Inject]
        private void Construct(PlayerHandler playerHandler, ICoroutineRunner coroutineRunner)
        {
            _playerHandler = playerHandler;
            _coroutineRunner = coroutineRunner;
        }

        public void OnSpawned(BoxCollider wanderingZone)
        {
            _wanderingZone = wanderingZone;
            _stateMachine = new NPCStateMachine();

            var wandering = new WanderingState(_coroutineRunner, _agent, _wanderingZone, _animator);
            var chassing = new ChassingState(_agent, _target, _animator);

            _stateMachine.AddTransition(wandering, chassing, EnemyIsInRange());
            _stateMachine.AddTransition(chassing, wandering, EnemyIsNotInRange());

            _stateMachine.EnterInState(wandering);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
        private Func<bool> EnemyIsNotInRange() => () => _target == null;
        private Func<bool> EnemyIsInRange() => () => _target != null;
    }
}
