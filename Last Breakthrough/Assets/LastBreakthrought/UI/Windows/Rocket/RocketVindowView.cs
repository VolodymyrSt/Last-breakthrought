using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using LastBreakthrought.Logic.ShipDetail;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.UI.Windows
{
    public class RocketVindowView : WindowView<RocketWindowHandler>
    {
        [Header("UI")]
        [SerializeField] private Button _repairButton;

        [Header("Container")]
        [SerializeField] private RectTransform _neededDetailsForRocketContainer;

        private ShipDetailsGeneratorUI _shipDetailsGeneratorUI;

        [Inject]
        private void Construct(ShipDetailsGeneratorUI shipDetailsGeneratorUI) =>
            _shipDetailsGeneratorUI = shipDetailsGeneratorUI;

        public override void Initialize()
        {
            _repairButton.onClick.AddListener(() => Handler.Rocket.TryToRepair());

            var requiredDetails = Handler.Rocket.GetDetailsToRepairRocket();
            _shipDetailsGeneratorUI.GenerateRequireDetails(requiredDetails, _neededDetailsForRocketContainer);
        }

        public override void Dispose() => 
            _repairButton.onClick.RemoveListener(() => Handler.Rocket.TryToRepair());
    }
}
