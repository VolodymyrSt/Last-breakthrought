
namespace LastBreakthrought.UI.Windows.CrashedShipWindow
{
    public class CrashedShipWindowView : WindowView<CrashedShipWindowHandler>
    {
        public override void Initialize()
        {
            Handler.CreateUnminedShipMaterialsView();

            Handler.MineEntireMaterial();
        }

        public override void Dispose()
        {

        }
    }
}
