using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip
{
    public class CrashedShip : MonoBehaviour, ICrashedShip
    {
        private CrashedShipsContainer _shipsContainer;

        [Inject]
        private void Construct(CrashedShipsContainer shipsContainer) => 
            _shipsContainer = shipsContainer;

        public void OnInitialized() =>
            _shipsContainer.CrashedShips.Add(this);

        public Vector3 GetPosition() => 
            transform.position;

        public void DestroySelf()
        {
            _shipsContainer.CrashedShips.Remove(this);
            Destroy(gameObject);
        }
    }
}
