using LastBreakthrought.Logic.ShipDetail;
using UnityEngine;

namespace LastBreakthrought.UI.Inventory.ShipDetail
{
    public class ShipDetailHandler : MonoBehaviour
    {
        [SerializeField] private ShipDetailView _shipDetailView;

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                _shipDetailView.SetQuantity(_quantity);
            }
        }

        public ShipDetailEntity DetailEntity { get; private set; }

        public void Init(ShipDetailEntity shipDetailEntity)
        {
            Quantity = shipDetailEntity.Quantity;
            DetailEntity = shipDetailEntity;

            _shipDetailView.SetQuantity(Quantity);
            _shipDetailView.SetImage(shipDetailEntity.Data.Sprite);
        }

        public void UpdateView(ShipDetailEntity shipDetail)
        {
            DetailEntity = shipDetail;

            _shipDetailView.SetImage(shipDetail.Data.Sprite);
        }

        public void SelfDesctroy() =>
            Destroy(gameObject);
    }
}
