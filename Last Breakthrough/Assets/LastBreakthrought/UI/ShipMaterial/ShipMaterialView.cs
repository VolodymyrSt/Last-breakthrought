using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LastBreakthrought.UI.ShipMaterial
{
    public class ShipMaterialView : MonoBehaviour
    {
        [SerializeField] private Image _shipMaterialImage;
        [SerializeField] private TextMeshProUGUI _quantityText;

        public void Setup(Sprite sprite, int quantity)
        {
            _shipMaterialImage.sprite = sprite;
            _quantityText.text = quantity.ToString();
        }
    }
}
