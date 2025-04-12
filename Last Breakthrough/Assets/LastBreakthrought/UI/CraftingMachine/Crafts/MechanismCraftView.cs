using System;
using UnityEngine;
using UnityEngine.UI;

namespace LastBreakthrought.UI.CraftingMachine.Crafts
{
    public class MechanismCraftView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Image _mechanismImage;
        [SerializeField] private RectTransform _detailsContainer;
        [SerializeField] private Button _craftButton;

        public void Init(Sprite sprite, Action craftAction)
        {
            _mechanismImage.sprite = sprite;
            _craftButton.onClick.AddListener(() => craftAction?.Invoke());
        }

        public RectTransform GetContainer() => _detailsContainer;
    }
}
