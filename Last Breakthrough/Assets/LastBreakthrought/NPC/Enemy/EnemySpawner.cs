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
        private void Construct(EnemyFactory enemyFactory, SpawnersContainer spawnersContainer)
        {
            _enemyFactory = enemyFactory;
            spawnersContainer.AddEnemySpawner(this);
        }

        public void SpawnEnemy()
        {
            var enemy = _enemyFactory.SpawnAt(transform.position, transform);
            enemy.OnSpawned(_wanderingZone, _enemyFactory.EnemyID);
        }
    }
}
