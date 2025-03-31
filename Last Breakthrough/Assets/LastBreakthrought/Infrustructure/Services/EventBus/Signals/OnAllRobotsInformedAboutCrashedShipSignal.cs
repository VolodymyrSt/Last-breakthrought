using LastBreakthrought.CrashedShip;

namespace LastBreakthrought.Infrustructure.Services.EventBus.Signals
{
    public class OnAllRobotsInformedAboutCrashedShipSignal
    {
        public ICrashedShip CrashedShip;
        public OnAllRobotsInformedAboutCrashedShipSignal(ICrashedShip crashedShip) { 
            CrashedShip = crashedShip;
        }
    }
}
