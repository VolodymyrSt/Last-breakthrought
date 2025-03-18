using UnityEngine;

namespace LastBreakthrought.UI.Windows.CrashedShipWindow
{
    public class CrashedShipWindowHandler : WindowHandler<CrashedShipWindowView>
    {
        [field: SerializeField] public CrashedShip.CrashedShip CrashedShip {  get; private set; }
        [field: SerializeField] public RectTransform ShipMaterialsContainer { get; private set; }

        public override void ActivateWindow()
        {
            View.ShowView();
        }

        public override void DeactivateWindow()
        {
            View.HideView();
        }
    }
}
