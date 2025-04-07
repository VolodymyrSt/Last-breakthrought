using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
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

        private ShipDetailUIFactory _shipDetailUIFactory;

        [Inject]
        private void Construct(ShipDetailUIFactory shipDetailUIFactory) =>
            _shipDetailUIFactory = shipDetailUIFactory;

        public override void Initialize()
        {
            _repairButton.onClick.AddListener(() => Handler.Rocket.TryToRepair());

            InitNeededDetailsForRocket();
        }

        private void InitNeededDetailsForRocket()
        {
            foreach (var neededDetail in Handler.Rocket.GetDetailsToRepairRocket())
            {
                var neededDitalView = _shipDetailUIFactory.SpawnAt(_neededDetailsForRocketContainer);
                neededDitalView.Init(neededDetail);
            }
        }

        public override void Dispose() => 
            _repairButton.onClick.RemoveListener(() => Handler.Rocket.TryToRepair());
    }
}
