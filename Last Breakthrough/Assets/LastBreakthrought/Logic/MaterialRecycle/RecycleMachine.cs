using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using LastBreakthrought.UI.Inventory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.MaterialRecycler
{
    public class RecycleMachine : MonoBehaviour
    {
        private ShipDetailsContainer _shipDetailsContainer;
        public List<ShipDetailEntity> ShipDetails { get; set; } = new List<ShipDetailEntity>();
        private DetailInventoryMenuPanelHandler _detailInventory;

        [Inject]
        private void Construct(ShipDetailsContainer shipDetailsContainer, DetailInventoryMenuPanelHandler detailInventoryMenuPanelHandler)
        {
            _shipDetailsContainer = shipDetailsContainer;
            _detailInventory = detailInventoryMenuPanelHandler;
        }

        public void RecycleEntireMaterial(ShipMaterialEntity shipMaterialEntity)
        {
            bool isNewDetail = true;

            foreach (var detail in ShipDetails)
            {
                if (shipMaterialEntity.Data.CraftDetail.Id == detail.Data.Id)
                {
                    detail.Quantity += shipMaterialEntity.Quantity;
                    _detailInventory.UpdateShipDetailsView(shipMaterialEntity);
                    isNewDetail = false;
                    break;
                }
            }

            if (isNewDetail)
            {
                var detail = _detailInventory.CreateNewShipDetailAndInit(shipMaterialEntity);
                ShipDetails.Add(detail.DetailEntity);
            }
        }

        public Vector3 GetMachinePosition() =>
            transform.position;
    }
}
