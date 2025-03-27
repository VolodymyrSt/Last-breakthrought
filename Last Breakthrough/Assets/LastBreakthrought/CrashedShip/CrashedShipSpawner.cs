using LastBreakthrought.Infrustructure;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip 
{
    public class CrashedShipSpawner : MonoBehaviour
    {
        private CrashedShipFactory _crashedShipFactory;

        [Inject]
        private void Construct(CrashedShipFactory crashedShipFactory, Game game)
        {
            _crashedShipFactory = crashedShipFactory;
            game.SpawnersContainer.AddCrashedShipSpawner(this);
        }

        public void SpawnCrashedShip() => 
            _crashedShipFactory.SpawnAt(transform.position, transform);
    }
}

