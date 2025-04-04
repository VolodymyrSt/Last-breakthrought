using LastBreakthrought.Logic.ShipMaterial;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using LastBreakthrought.UI.Windows.CrashedShipWindow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip
{
    public class CrashedShip : MonoBehaviour, ICrashedShip
    {
        private const float DESCTRUCTION_TIME = 5f;
        [SerializeField] private ShipRarity _rarity;
        [SerializeField] private int _maxNumberOfMaterialDiversity;
        [SerializeField] private CrashedShipWindowHandler _crashedShipWindowHandler;

        private CrashedShipsContainer _shipsContainer;
        private ShipMaterialGenerator _shipMaterialGenerator;

        public List<ShipMaterialEntity> Materials { get; private set; } = new ();
        public List<ShipMaterialEntity> MinedMaterials { get; private set; } = new ();

        private void OnValidate()
        {
            if (_maxNumberOfMaterialDiversity < (int)_rarity)
                _maxNumberOfMaterialDiversity = (int)_rarity;
            if (_maxNumberOfMaterialDiversity > Constants.MaxNumberOfShipMaterialsInOneWindow)
                _maxNumberOfMaterialDiversity = Constants.MaxNumberOfShipMaterialsInOneWindow;
        }

        [Inject]
        private void Construct(CrashedShipsContainer shipsContainer, ShipMaterialGenerator materialGenerator)
        {
            _shipsContainer = shipsContainer;
            _shipMaterialGenerator = materialGenerator;
        }

        public void OnInitialized()
        {
            _shipsContainer.CrashedShips.Add(this);
            Materials = _shipMaterialGenerator.GenerateShipMaterials(_rarity, _maxNumberOfMaterialDiversity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                MineEntireMaterial();
        }

        public Vector3 GetPosition() => 
            transform.position;

        public ShipMaterialEntity MineEntireMaterial()
        {
            ShipMaterialEntity minedMaterial = null;
            foreach (var unminedShipMaterial in Materials)
            {
                if (Materials.Count < 0)
                    break;
                else
                {
                    minedMaterial = unminedShipMaterial;
                    _crashedShipWindowHandler.UpdateEntireMaterial();
                    MinedMaterials.Add(unminedShipMaterial);
                    Materials.Remove(unminedShipMaterial);
                    break;
                }
            }
            return minedMaterial;
        }

        public void RemoveMinedMaterialView() => 
            _crashedShipWindowHandler.RemoveMinedMaterialFromWindow();

        public IEnumerator DestroySelf()
        {
            yield return new WaitForSecondsRealtime(DESCTRUCTION_TIME);
            _shipsContainer.CrashedShips.Remove(this);
            Destroy(gameObject);
        }
    }
}
