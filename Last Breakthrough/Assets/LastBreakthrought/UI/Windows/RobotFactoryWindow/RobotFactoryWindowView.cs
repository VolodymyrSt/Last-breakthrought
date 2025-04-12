using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using LastBreakthrought.Logic.Mechanisms;
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
        [SerializeField] private RectTransform _neededMechanismsForMinerCreateContainer;
        [SerializeField] private RectTransform _neededMechanismsForTransporterCreateContainer;

        private MechanismsGeneratorUI _mechanismsGeneratorUI;

        [Inject]
        private void Construct(MechanismsGeneratorUI mechanismsGeneratorUI) =>
            _mechanismsGeneratorUI = mechanismsGeneratorUI;

        public override void Initialize()
        {
            _createRobotMinerButton.onClick.AddListener(() =>
                Handler.CreateMiner());

            _createRobotTransporterButton.onClick.AddListener(() =>
                Handler.CreateTransporter());

            GenerateRequiredMechanismsForCreatingRobots();
        }

        private void GenerateRequiredMechanismsForCreatingRobots()
        {
            var requiredMechanismsForMiner = Handler.RobotFactoryMachine.GetMechanismsToCreateMiner();
            _mechanismsGeneratorUI.GenerateRequireMechanisms(requiredMechanismsForMiner, _neededMechanismsForMinerCreateContainer);

            var requiredMechanismsForTransporter = Handler.RobotFactoryMachine.GetMechanismsToCreateTransporter();
            _mechanismsGeneratorUI.GenerateRequireMechanisms(requiredMechanismsForTransporter, _neededMechanismsForTransporterCreateContainer);
        }

        public override void Dispose()
        {
            _createRobotMinerButton.onClick.RemoveListener(() => Handler.CreateMiner());
            _createRobotTransporterButton.onClick.RemoveListener(() => Handler.CreateTransporter());
        }
    }
}

