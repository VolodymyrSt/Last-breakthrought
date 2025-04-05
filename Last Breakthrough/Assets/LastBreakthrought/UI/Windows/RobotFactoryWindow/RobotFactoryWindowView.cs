using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.UI.Windows.RobotFactoryWindow
{
    public class RobotFactoryWindowView : WindowView<RobotFactoryWindowHandler>
    {
        [Header("UI")]
        [SerializeField] private Button _createRobotMinerButton;
        [SerializeField] private Button _createRobotTransporterButton;

        [Header("Containers")]
        [SerializeField] private RectTransform _neededDetailsForMinerCreateContainer;
        [SerializeField] private RectTransform _neededDetailsForTransporterCreateContainer;

        private ShipDetailUIFactory _shipDetailUIFactory;

        [Inject]
        private void Construct(ShipDetailUIFactory shipDetailUIFactory) => 
            _shipDetailUIFactory = shipDetailUIFactory;

        public override void Initialize()
        {
            _createRobotMinerButton.onClick.AddListener(() =>
                Handler.CreateMiner());

            _createRobotTransporterButton.onClick.AddListener(() =>
                Handler.CreateTransporter());

            InitNeededDetailsForCreatingRobots();
        }

        private void InitNeededDetailsForCreatingRobots()
        {
            foreach (var neededDetail in Handler.RobotFactoryMachine.GetDetailsToCreateMiner())
            {
                var neededDitalView = _shipDetailUIFactory.SpawnAt(_neededDetailsForMinerCreateContainer);
                neededDitalView.Init(neededDetail);
            }

            foreach (var neededDetail in Handler.RobotFactoryMachine.GetDetailsToCreateTransporter())
            {
                var neededDitalView = _shipDetailUIFactory.SpawnAt(_neededDetailsForTransporterCreateContainer);
                neededDitalView.Init(neededDetail);
            }
        }

        public override void Dispose()
        {
            _createRobotMinerButton.onClick.RemoveListener(() => Handler.CreateMiner());
            _createRobotTransporterButton.onClick.RemoveListener(() => Handler.CreateTransporter());
        }
    }
}

