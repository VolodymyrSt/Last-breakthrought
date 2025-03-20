using UnityEngine;

namespace LastBreakthrought.Infrustructure.AssetManagment
{
    public static class AssetPath
    {
        public static string CrashedShipPath = "CrashedShips/CrashedShip_Big";
        public static string ShipMaterialViewPath = "ShipMaterial/ShipMaterial";
        public static string GolemPath = "NPC/Enemies/Golem";

        public static string GetRandomEnemyPath()
        {
            return GolemPath;
        }
    }
}
