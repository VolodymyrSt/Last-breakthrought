using DG.Tweening;
using LastBreakthrought.UI.SlotItem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LastBreakthrought.UI.Slot
{
    public class SlotView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Item _item;
        [SerializeField] private RectTransform _selectedFrame;

        private bool _isSelected = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isSelected)
            {
                _isSelected = true;
                _selectedFrame.gameObject.SetActive(true);
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                _item.Select();
            }
            else
            {
                _isSelected = false;
                _selectedFrame.gameObject.SetActive(false);
                _item.UnSelect();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isSelected) return;

            var scaledSize = 1.1f;
            transform.DOScale(scaledSize, 0.2f)
            .SetEase(Ease.InOutCubic)
            .Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected) return;

            var normalSize = 1f;

            transform.DOScale(normalSize, 0.2f)
            .SetEase(Ease.InOutCubic)
            .Play();
        }
    }
}
