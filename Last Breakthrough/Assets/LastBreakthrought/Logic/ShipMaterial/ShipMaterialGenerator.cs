using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using System.Collections.Generic;
using System;
using LastBreakthrought.CrashedShip;

namespace LastBreakthrought.Logic.ShipMaterial
{
    public class ShipMaterialGenerator
    {
        private List<ShipMaterialSO> _availableMaterials;

        public ShipMaterialGenerator(List<ShipMaterialSO> materials) =>
            _availableMaterials = new List<ShipMaterialSO>(materials);

        public List<ShipMaterialEntity> GenerateShipMaterials(ShipRarity shipRarity, int maxNumberOfMaterialDiversity)
        {
            if (maxNumberOfMaterialDiversity < (int)shipRarity)
                throw new InvalidOperationException($"The parametr {maxNumberOfMaterialDiversity} was not correct.");

            List<ShipMaterialEntity> materialEntities = new List<ShipMaterialEntity>();
            int numberOfMaterialDiversity = UnityEngine.Random.Range((int)shipRarity, maxNumberOfMaterialDiversity);

            var selectedMaterials = GetRandomShipMaterialsData(numberOfMaterialDiversity);

            foreach (var material in selectedMaterials)
            {
                int quantity = UnityEngine.Random.Range((int)shipRarity, material.MaxQuantity);
                materialEntities.Add(new ShipMaterialEntity(material, quantity));
            }

            return materialEntities;
        }

        private List<ShipMaterialSO> GetRandomShipMaterialsData(int countOfMaterialDiversity)
        {
            if (_availableMaterials.Count < countOfMaterialDiversity)
                throw new InvalidOperationException("Insufficient unique materials available.");

            List<ShipMaterialSO> availableMaterials = new List<ShipMaterialSO>(_availableMaterials); 
            List<ShipMaterialSO> uniqueMaterials = new List<ShipMaterialSO>();

            while (uniqueMaterials.Count < countOfMaterialDiversity)
            {
                var randomMaterial = availableMaterials[UnityEngine.Random.Range(0, availableMaterials.Count)];

                uniqueMaterials.Add(randomMaterial);
                availableMaterials.Remove(randomMaterial);
            }

            return uniqueMaterials;
        }
    }
}
