using LastBreakthrought.Infrustructure.AssetManagment;
using LastBreakthrought.Player;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip
{
    public class CrashedShip : MonoBehaviour, ICrashedShip
    {
        private IAssetProvider handler;

        [Inject]
        private void Construct(IAssetProvider playerHandler)
        {
            handler = playerHandler;
        }

        public void DestroySelf() => 
            Destroy(gameObject);
    }
}
