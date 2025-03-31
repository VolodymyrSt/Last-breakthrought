using LastBreakthrought.Configs.Robot;
using LastBreakthrought.CrashedShip;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.NPC.Robot;
using LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel.RobotControls;
using LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel.RobotControls.Factory;
using System;
using System.Collections.Generic;
using Zenject;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel
{
    public class RobotMenuPanelHandler : IInitializable, ITickable
    {
        public RobotMenuPanelView View {  get; private set; }

        private readonly RobotMinerControlUIFactory _robotMinerControlUIFactory;
        private readonly RobotTransporterControlUIFactory _robotTransporterControlUIFactory;
        public readonly IEventBus _eventBus;

        private readonly List<RobotControlHandlerUI> _robotControls = new ();

        public RobotMenuPanelHandler(RobotMenuPanelView view, IEventBus eventBus, RobotMinerControlUIFactory robotControlUIFactory, RobotTransporterControlUIFactory robotTransporterControlUIFactory)
        {
            View = view;
            _robotMinerControlUIFactory = robotControlUIFactory;
            _robotTransporterControlUIFactory = robotTransporterControlUIFactory;
            _eventBus = eventBus;
        }

        public void Initialize() => View.Init();

        public void Tick()
        {
            foreach (var robotControl in _robotControls)
                robotControl.UpdateSlider();
        }

        public void AddRobotMinerControlUI(RobotConfigSO robotData, RobotBattary battary, Action followAction, Action goHomeAction, Action mineAction)
        {
            var robotControl = _robotMinerControlUIFactory.Create(View.GetContainer(),
                robotData, battary, followAction, goHomeAction, mineAction);

            _robotControls.Add(robotControl);
        }
        
        public void AddRobotTransporterControlUI(RobotConfigSO robotData, RobotBattary battary, Action followAction, Action goHomeAction, Action transportAction)
        {
            var robotControl = _robotTransporterControlUIFactory.Create(View.GetContainer(),
                robotData, battary, followAction, goHomeAction, transportAction);

            _robotControls.Add(robotControl);
        }

        public void InformAllRobotsAboutCrashedShip(ICrashedShip crashedShip) => 
            _eventBus.Invoke(new OnAllRobotsInformedAboutCrashedShipSignal(crashedShip));
    }
}
