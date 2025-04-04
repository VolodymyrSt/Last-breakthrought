using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastBreakthrought.CrashedShip
{
    public interface ICrashedShip
    {
        List<ShipMaterialEntity> Materials { get; }
        List<ShipMaterialEntity> MinedMaterials { get; }

        Vector3 GetPosition();
        void OnInitialized();
        ShipMaterialEntity MineEntireMaterial();
        IEnumerator DestroySelf();
        void RemoveMinedMaterialView();
    }
}
