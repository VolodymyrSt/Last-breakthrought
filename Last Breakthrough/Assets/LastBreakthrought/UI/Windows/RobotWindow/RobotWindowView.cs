using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.UI.RobotWindow
{
    public class RobotWindowView : WindowView<RobotWindowHandler>
    {
        [Header("UI")]
        [SerializeField] private RectTransform _detailsContainerForRepair;
        [SerializeField] private Button _repairButton;

        private ShipDetailsGeneratorUI _shipDetailsGeneratorUI;

        [Inject]
        private void Construct(ShipDetailsGeneratorUI shipDetailsGeneratorUI) =>
            _shipDetailsGeneratorUI = shipDetailsGeneratorUI;

        public override void Initialize()
        {
            var detailsToRepairRobot = Handler.Robot.GetRequiredDetailsToRepair();
            _shipDetailsGeneratorUI.GenerateRequireDetails(detailsToRepairRobot, _detailsContainerForRepair);

            _repairButton.onClick.AddListener(() => Handler.Robot.TryToRepair());
        }   

        public override void Dispose() =>
            _repairButton.onClick.RemoveListener(() => Handler.Robot.TryToRepair());
    }
}

