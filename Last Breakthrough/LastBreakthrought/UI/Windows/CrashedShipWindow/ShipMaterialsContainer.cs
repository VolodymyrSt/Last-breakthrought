using LastBreakthrought.UI.ShipMaterial;
using System.Collections.Generic;
using UnityEngine;

namespace LastBreakthrought.UI.Windows.CrashedShipWindow
{
    public class ShipMaterialsContainer : MonoBehaviour
    {
        public List<ShipMaterialHandler> Materials { get; set; } = new();
    }
}
