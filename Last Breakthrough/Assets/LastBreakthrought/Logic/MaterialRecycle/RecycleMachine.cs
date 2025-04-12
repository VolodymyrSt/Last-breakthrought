using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using LastBreakthrought.UI.Inventory;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.MaterialRecycler
{
    public class RecycleMachine : MonoBehaviour
    {
        private DetailsContainer _detailsContainer;
        private InventoryMenuPanelHandler _detailInventory;

        [Inject]
        private void Construct(DetailsContainer shipDetailsContainer, InventoryMenuPanelHandler detailInventoryMenuPanelHandler)
        {
            _detailsContainer = shipDetailsContainer;
            _detailInventory = detailInventoryMenuPanelHandler;
        }

        public void RecycleEntireMaterial(ShipMaterialEntity shipMaterialEntity)
        {
            bool isNewDetail = true;

            foreach (var detail in _detailsContainer.Details)
            {
                if (shipMaterialEntity.Data.CraftDetail.Id == detail.Data.Id)
                {
                    detail.Quantity += shipMaterialEntity.Quantity;
                    _detailInventory.UpdateInventoryDetails(shipMaterialEntity);
                    isNewDetail = false;
                    break;
                }
            }

            if (isNewDetail)
            {
                var detail = _detailInventory.CreateNewShipDetailAndInit(shipMaterialEntity);
                _detailsContainer.Details.Add(detail.DetailEntity);
            }
        }

        public Vector3 GetMachinePosition() =>
            transform.position;
    }
}
