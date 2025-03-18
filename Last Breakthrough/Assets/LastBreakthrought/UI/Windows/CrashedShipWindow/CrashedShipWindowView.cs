using LastBreakthrought.Logic.ShipMaterial;
using Zenject;

namespace LastBreakthrought.UI.Windows.CrashedShipWindow
{
    public class CrashedShipWindowView : WindowView<CrashedShipWindowHandler>
    {
        private ShipMaterialViewFactory _shipMaterialViewFactory;

        [Inject]
        private void Construct(ShipMaterialViewFactory shipMaterialViewFactory) => 
            _shipMaterialViewFactory = shipMaterialViewFactory;

        public override void Initialize()
        {
            foreach (var shipMaterial in Handler.CrashedShip.ShipMaterials)
            {
                var ShipMaterialView = _shipMaterialViewFactory.SpawnAt(Handler.ShipMaterialsContainer);
                ShipMaterialView.Init(shipMaterial);
            }
        }
        public override void Dispose()
        {

        }
    }
}
