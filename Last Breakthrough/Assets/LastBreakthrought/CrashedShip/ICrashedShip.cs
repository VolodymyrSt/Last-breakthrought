using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace LastBreakthrought.CrashedShip
{
    public interface ICrashedShip
    {
        List<ShipMaterialEntity> Materials { get; }
        Vector3 GetPosition();
        void OnInitialized();
        ShipMaterialEntity MineEntireMaterial();
        void DestroySelf();
    }
}
