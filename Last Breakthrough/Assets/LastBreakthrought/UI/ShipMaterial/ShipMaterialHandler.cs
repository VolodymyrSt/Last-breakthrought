using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using UnityEngine;

namespace LastBreakthrought.UI.ShipMaterial
{
    public class ShipMaterialHandler : MonoBehaviour
    {
        [SerializeField] private ShipMaterialView _shipMaterialView;

        private int Quantity { get; set; }

        public void Init(ShipMaterialEntity materialEntity)
        {
            _shipMaterialView.Setup(materialEntity.Data.Sprite, materialEntity.Quantity);
            Quantity = materialEntity.Quantity;
        }
    }
}
