using UnityEngine;
using LastBreakthrought.Logic.RobotFactory;

namespace LastBreakthrought.UI.Windows.RobotFactoryWindow 
{
    public class RobotFactoryWindowHandler : WindowHandler<RobotFactoryWindowView>
    {
        [SerializeField] private RobotFactoryMachine _robotFactoryMachine;

        public override void ActivateWindow() => View.ShowView();
        public override void DeactivateWindow() => View.HideView();

        public void CreateMiner() =>
            _robotFactoryMachine.CreateRobotMiner();
        public void CreateTransporter() =>
            _robotFactoryMachine.CreateRobotTransporter();
    }
}

