using LastBreakthrought.NPC.Enemy.Factory;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.NPC.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private BoxCollider _wanderingZone;

        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(EnemyFactory enemyFactory) => 
            _enemyFactory = enemyFactory;

        private void Start()
        {
            var enemy = _enemyFactory.SpawnAt(transform.position, transform);
            enemy.OnSpawned(_wanderingZone);
        }
    }
}
