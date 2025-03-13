using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip 
{
    public class CrashedShipSpawner : MonoBehaviour
    {
        private CrashedShipFactory _crashedShipFactory;

        [Inject]
        private void Construct(CrashedShipFactory crashedShipFactory) => 
            _crashedShipFactory = crashedShipFactory;

        private void Awake()
        {
            _crashedShipFactory.SpawnAt(transform.position, transform);
        }
    }
}

