using System;
using UnityEngine;

namespace LastBreakthrought.Infrustructure.AssetManagment
{
    public static class AssetPath
    {
        public static string CrashedShipPath = "CrashedShips/CrashedShip_Big";
        public static string ShipMaterialViewPath = "ShipMaterial/ShipMaterial";
        public static string GolemPath = "NPC/Enemies/Golem";
        public static string BatPath = "NPC/Enemies/Bat";

        public static string GetRandomEnemyPath()
        {
            var randomPath = UnityEngine.Random.Range(1, 3);

            if (randomPath == 1)
                return GolemPath;
            if (randomPath == 2) 
                return BatPath;

            throw new Exception("The enemyPath doesnt exist");
        }
    }
}
