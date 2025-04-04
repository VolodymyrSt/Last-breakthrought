using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.Logic.ShipMaterial;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using LastBreakthrought.UI.Inventory.ShipDetail;
using LastBreakthrought.UI.ShipMaterial;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.UI.Inventory
{
    public class DetailInventoryMenuPanelHandler : IInitializable
    {
        private readonly DetailInventoryMenuPanelView _view;
        private readonly ShipDetailUIFactory _shipDetailUIFactory;

        public DetailInventoryMenuPanelHandler(DetailInventoryMenuPanelView view, ShipDetailUIFactory shipDetailUIFactory)
        {
            _view = view;
            _shipDetailUIFactory = shipDetailUIFactory;
        }

        public void Initialize() => _view.Init();

        public void UpdateShipDetailsView(ShipMaterialEntity shipMaterial)
        {
            foreach (var shipDetail in _view.DetailsContainerUI.Details)
            {
                if (shipMaterial.Data.CraftDetail.Id == shipDetail.DetailEntity.Data.Id)
                {
                    shipDetail.Quantity += shipMaterial.Quantity;
                    shipDetail.UpdateView(shipDetail.DetailEntity);
                    break;
                }
            }        
        }

        public ShipDetailHandler CreateNewShipDetailAndInit(ShipMaterialEntity shipMaterial)
        {
            var shipDetailUI = _shipDetailUIFactory.SpawnAt(_view.GetContainer());
            var shipDetailEntity = new ShipDetailEntity(shipMaterial.Data.CraftDetail, shipMaterial.Quantity);
            _view.DetailsContainerUI.Details.Add(shipDetailUI);
            shipDetailUI.Init(shipDetailEntity);
            return shipDetailUI;
        }
    }
}
