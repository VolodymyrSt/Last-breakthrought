using LastBreakthrought.Logic.ShipMaterial;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.CrashedShip
{
    public class CrashedShip : MonoBehaviour, ICrashedShip
    {
        [SerializeField] private ShipRarity _rarity;
        [SerializeField] private int _maxNumberOfMaterialDiversity;

        private CrashedShipsContainer _shipsContainer;
        private ShipMaterialGenerator _shipMaterialGenerator;

        public List<ShipMaterialEntity> ShipMaterials { get; private set; } = new ();

        private void OnValidate()
        {
            if (_maxNumberOfMaterialDiversity <= (int)_rarity)
                _maxNumberOfMaterialDiversity = (int)_rarity + 1;
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
            ShipMaterials = _shipMaterialGenerator.GenerateShipMaterials(_rarity, _maxNumberOfMaterialDiversity);
        }

        public Vector3 GetPosition() => 
            transform.position;

        public void DestroySelf()
        {
            _shipsContainer.CrashedShips.Remove(this);
            Destroy(gameObject);
        }
    }
}
