using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Infrustructure.Services.Massage;
using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.UI.Inventory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.Rocket
{
    public class Rocket : MonoBehaviour
    {
        [Header("DetailsForRocketRepairing:")]
        [SerializeField] private RequireDetailsForCreating _neededDetailsToRepairRocket;

        private DetailsContainer _detailsContainer;
        private DetailInventoryMenuPanelHandler _detailInventory;
        private IEventBus _eventBus;
        private IMassageHandlerService _massageHandler;

        [Inject]
        private void Construct(DetailsContainer detailsContainer, DetailInventoryMenuPanelHandler detailInventory
            , IEventBus eventBus, IMassageHandlerService massageHandler)
        {
            _detailsContainer = detailsContainer;
            _detailInventory = detailInventory;
            _eventBus = eventBus;
            _massageHandler = massageHandler;
        }

        public void TryToRepair()
        {
            if (_detailsContainer.IsSearchedDetailsAllFound(GetDetailsToRepairRocket()))
                Repair();
            else
                _massageHandler.ShowMassage("You don`t have the right details");
        }

        public List<ShipDetailEntity> GetDetailsToRepairRocket() =>
            _neededDetailsToRepairRocket.GetRequiredShipDetails();

        private void Repair()
        {
            _detailsContainer.GiveDetails(GetDetailsToRepairRocket());
            _detailInventory.UpdateInventoryDetails(GetDetailsToRepairRocket());
            _eventBus.Invoke(new OnGameWonSignal());
        }
    }
}
