using LastBreakthrought.Logic.ShipMaterial;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using LastBreakthrought.UI.ShipMaterial;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.UI.Windows.CrashedShipWindow
{
    public class CrashedShipWindowHandler : WindowHandler<CrashedShipWindowView>
    {
        [field: SerializeField] public CrashedShip.CrashedShip CrashedShip {  get; private set; }
        [field: SerializeField] public ShipMaterialsContainer UnminedShipMaterialsContainer { get; private set; }
        [field: SerializeField] public ShipMaterialsContainer MinedShipMaterialsContainer { get; private set; }

        private ShipMaterialUIFactory _shipMaterialUIFactory;

        private RectTransform _unminedShipMaterialsContainerTransform;
        private RectTransform _minedShipMaterialsContainerTransform;

        [Inject]
        private void Construct(ShipMaterialUIFactory shipMaterialUIFactory) =>
            _shipMaterialUIFactory = shipMaterialUIFactory;

        private void OnEnable()
        {
            _unminedShipMaterialsContainerTransform = UnminedShipMaterialsContainer.GetComponent<RectTransform>();
            _minedShipMaterialsContainerTransform = MinedShipMaterialsContainer.GetComponent<RectTransform>();
        }

        public override void ActivateWindow() => View.ShowView();
        public override void DeactivateWindow() => View.HideView();

        public void CreateUnminedShipMaterialsView()
        {
            foreach (var unminedShipMaterial in CrashedShip.Materials)
                CreateUnminedShipMaterialAndInit(unminedShipMaterial);
        }

        public void MineOneMaterial()
        {
            bool isMaterialNew = true;

            foreach (var unminedShipMaterial in UnminedShipMaterialsContainer.Materials)
            {
                if (UnminedShipMaterialsContainer.Materials.Count < 0)
                    break;

                foreach (var minedShipMaterial in MinedShipMaterialsContainer.Materials)
                {
                    if (MinedShipMaterialsContainer.Materials.Count < 0)
                        break;

                    if (unminedShipMaterial.MaterialEntity.Data.Id == minedShipMaterial.MaterialEntity.Data.Id)
                    {
                        IncreaseMinedShipMaterial(unminedShipMaterial, minedShipMaterial);
                        isMaterialNew = false;
                        break;
                    }
                }

                if (isMaterialNew)
                {
                    CreateNewMinedShipMaterialAndInit(unminedShipMaterial);
                    break;
                }
            }
        }

        public void UpdateEntireMaterial()
        {
            foreach (var unminedShipMaterial in UnminedShipMaterialsContainer.Materials)
            {
                if (UnminedShipMaterialsContainer.Materials.Count < 0)
                    break;

                var newShipMaterialUI = _shipMaterialUIFactory.SpawnAt(MinedShipMaterialsContainer.GetComponent<RectTransform>());
                newShipMaterialUI.InitMined(unminedShipMaterial.MaterialEntity);
                newShipMaterialUI.Quantity = unminedShipMaterial.Quantity;

                MinedShipMaterialsContainer.Materials.Add(newShipMaterialUI);
                UnminedShipMaterialsContainer.Materials.Remove(unminedShipMaterial);
                Destroy(unminedShipMaterial.gameObject);
                break;
            }
        }

        private void IncreaseMinedShipMaterial(ShipMaterialHandler unminedShipMaterial, ShipMaterialHandler minedShipMaterial)
        {
            unminedShipMaterial.Quantity--;

            if (unminedShipMaterial.Quantity <= 0)
            {
                UnminedShipMaterialsContainer.Materials.Remove(unminedShipMaterial);
                Destroy(unminedShipMaterial.gameObject);
            }

            minedShipMaterial.Quantity++;
        }

        private void CreateNewMinedShipMaterialAndInit(ShipMaterialHandler unminedShipMaterial)
        {
            unminedShipMaterial.Quantity--;

            if (unminedShipMaterial.Quantity <= 0)
            {
                UnminedShipMaterialsContainer.Materials.Remove(unminedShipMaterial);
                Destroy(unminedShipMaterial.gameObject);
            }

            var newShipMaterialUI = _shipMaterialUIFactory.SpawnAt(_minedShipMaterialsContainerTransform);

            newShipMaterialUI.Quantity = 1;
            newShipMaterialUI.InitMined(unminedShipMaterial.MaterialEntity);

            MinedShipMaterialsContainer.Materials.Add(newShipMaterialUI);
        }

        private void CreateUnminedShipMaterialAndInit(ShipMaterialEntity unminedShipMaterial)
        {
            var shipMaterialUI = _shipMaterialUIFactory.SpawnAt(_unminedShipMaterialsContainerTransform);
            UnminedShipMaterialsContainer.Materials.Add(shipMaterialUI);
            shipMaterialUI.Init(unminedShipMaterial);
        }
    }
}
