using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using LastBreakthrought.Logic.ShipDetail;
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

        private ShipDetailsGeneratorUI _shipDetailsGeneratorUI;

        [Inject]
        private void Construct(ShipDetailsGeneratorUI shipDetailsGeneratorUI) =>
            _shipDetailsGeneratorUI = shipDetailsGeneratorUI;

        public override void Initialize()
        {
            _createRobotMinerButton.onClick.AddListener(() =>
                Handler.CreateMiner());

            _createRobotTransporterButton.onClick.AddListener(() =>
                Handler.CreateTransporter());

            GenerateRequiredDetailsForCreatingRobots();
        }

        private void GenerateRequiredDetailsForCreatingRobots()
        {
            var requiredDetailForMiner = Handler.RobotFactoryMachine.GetDetailsToCreateMiner();
            _shipDetailsGeneratorUI.GenerateRequireDetails(requiredDetailForMiner, _neededDetailsForMinerCreateContainer);

            var requiredDetailForTransporter = Handler.RobotFactoryMachine.GetDetailsToCreateTransporter();
            _shipDetailsGeneratorUI.GenerateRequireDetails(requiredDetailForTransporter, _neededDetailsForTransporterCreateContainer);
        }

        public override void Dispose()
        {
            _createRobotMinerButton.onClick.RemoveListener(() => Handler.CreateMiner());
            _createRobotTransporterButton.onClick.RemoveListener(() => Handler.CreateTransporter());
        }
    }
}

