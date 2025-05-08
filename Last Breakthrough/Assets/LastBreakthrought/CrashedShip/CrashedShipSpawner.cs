using LastBreakthrought.Infrustructure;
using System.Collections;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip 
{
    public class CrashedShipSpawner : MonoBehaviour
    {
        private const float SPAWNING_TIME_AFTER_DESTRUCTION = 120f;
        private CrashedShipFactory _crashedShipFactory;
        private ICrashedShip _spawnedCrashedShip;

        [Inject]
        private void Construct(CrashedShipFactory crashedShipFactory, Game game)
        {
            _crashedShipFactory = crashedShipFactory;
            game.SpawnersContainer.AddCrashedShipSpawner(this);
        }

        public void SpawnCrashedShip()
        {
            _spawnedCrashedShip = _crashedShipFactory.SpawnAt(transform.position, transform);
            _spawnedCrashedShip.OnDestroyed += Respawn;
        }

        private void Respawn() =>
            StartCoroutine(SpawnShipAfterTime());

        private IEnumerator SpawnShipAfterTime()
        {
            yield return new WaitForSeconds(SPAWNING_TIME_AFTER_DESTRUCTION);
            SpawnCrashedShip();
        }
    }
}

