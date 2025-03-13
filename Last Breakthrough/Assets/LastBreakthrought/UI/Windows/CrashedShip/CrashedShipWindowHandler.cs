namespace LastBreakthrought.UI.Windows.CrashedShip
{
    public class CrashedShipWindowHandler : WindowHandler<CrashedShipWindowView>
    {

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
