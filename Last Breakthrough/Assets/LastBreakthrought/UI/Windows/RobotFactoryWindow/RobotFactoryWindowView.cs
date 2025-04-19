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
        [SerializeField] private Button _createRobotDefenderButton;

        [Header("Containers")]
        [SerializeField] private RectTransform _neededMechanismsForMinerCreateContainer;
        [SerializeField] private RectTransform _neededMechanismsForTransporterCreateContainer;
        [SerializeField] private RectTransform _neededMechanismsForDefenderCreateContainer;

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

            _createRobotDefenderButton.onClick.AddListener(() =>
                Handler.CreateDefender());

            GenerateRequiredMechanismsForCreatingRobots();
        }

        private void GenerateRequiredMechanismsForCreatingRobots()
        {
            var requiredMechanismsForMiner = Handler.RobotFactoryMachine.GetMechanismsToCreateMiner();
            _mechanismsGeneratorUI.GenerateRequireMechanisms(requiredMechanismsForMiner, _neededMechanismsForMinerCreateContainer);

            var requiredMechanismsForTransporter = Handler.RobotFactoryMachine.GetMechanismsToCreateTransporter();
            _mechanismsGeneratorUI.GenerateRequireMechanisms(requiredMechanismsForTransporter, _neededMechanismsForTransporterCreateContainer);

            var requiredMechanismsForDefender = Handler.RobotFactoryMachine.GetMechanismsToCreateDefender();
            _mechanismsGeneratorUI.GenerateRequireMechanisms(requiredMechanismsForDefender, _neededMechanismsForDefenderCreateContainer);
        }

        public override void Dispose()
        {
            _createRobotMinerButton.onClick.RemoveListener(() => Handler.CreateMiner());
            _createRobotTransporterButton.onClick.RemoveListener(() => Handler.CreateTransporter());
            _createRobotTransporterButton.onClick.RemoveListener(() => Handler.CreateDefender());
        }
    }
}

