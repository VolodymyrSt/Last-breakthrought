using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using UnityEngine;

namespace LastBreakthrought.UI.ShipMaterial
{
    public class ShipMaterialHandler : MonoBehaviour
    {
        [SerializeField] private ShipMaterialView _shipMaterialView;

        private int _quantity; 
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                _shipMaterialView.SetQuantity(_quantity);
            }
        }

        public ShipMaterialEntity MaterialEntity { get; private set; }

        public void Init(ShipMaterialEntity materialEntity)
        {
            Quantity = materialEntity.Quantity;
            MaterialEntity = materialEntity;

            _shipMaterialView.SetQuantity(Quantity);
            _shipMaterialView.SetImage(materialEntity.Data.Sprite);
        }
        
        public void InitMined(ShipMaterialEntity materialEntity)
        {
            MaterialEntity = materialEntity;

            _shipMaterialView.SetImage(materialEntity.Data.Sprite);
        }
    }
}
