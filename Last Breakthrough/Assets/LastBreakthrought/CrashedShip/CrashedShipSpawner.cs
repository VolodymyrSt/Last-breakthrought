using LastBreakthrought.Other;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip 
{
    public class CrashedShipSpawner : MonoBehaviour
    {
        private CrashedShipFactory _crashedShipFactory;

        [Inject]
        private void Construct(CrashedShipFactory crashedShipFactory, SpawnersContainer spawnersContainer)
        {
            _crashedShipFactory = crashedShipFactory;
            spawnersContainer.AddCrashedShipSpawner(this);
        }

        public void SpawnCrashedShip() => 
            _crashedShipFactory.SpawnAt(transform.position, transform);
    }
}

