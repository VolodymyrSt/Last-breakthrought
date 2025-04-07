using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using LastBreakthrought.UI.Inventory.ShipDetail;
using System.Collections.Generic;
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

        public void UpdateInventoryDetails(ShipMaterialEntity shipMaterial)
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

        public void UpdateInventoryDetails(List<ShipDetailEntity> neededDetails)
        {
            foreach (var neededDetail in neededDetails)
            {
                for (var i = 0; i < _view.DetailsContainerUI.Details.Count; i++)
                {
                    var existedDetailView = _view.DetailsContainerUI.Details[i];

                    if (existedDetailView != null)
                    {
                        if (existedDetailView.DetailEntity.Data.Id == neededDetail.Data.Id)
                        {
                            existedDetailView.Quantity -= neededDetail.Quantity;

                            if (existedDetailView.Quantity <= 0)
                            {
                                existedDetailView.SelfDesctroy();
                                _view.DetailsContainerUI.Details.Remove(existedDetailView);
                            }
                        }
                    }
                }
            }
        }
    }
}
