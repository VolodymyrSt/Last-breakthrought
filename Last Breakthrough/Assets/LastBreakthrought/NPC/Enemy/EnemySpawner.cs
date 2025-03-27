using LastBreakthrought.Infrustructure;
using LastBreakthrought.NPC.Enemy.Factory;
using LastBreakthrought.Other;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.NPC.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private BoxCollider _wanderingZone;

        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(EnemyFactory enemyFactory, Game game)
        {
            _enemyFactory = enemyFactory;
            game.SpawnersContainer.AddEnemySpawner(this);
        }

        public void SpawnEnemy()
        {
            var enemy = _enemyFactory.SpawnAt(transform.position, transform);
            enemy.OnSpawned(_wanderingZone, _enemyFactory.EnemyID);
        }
    }
}
