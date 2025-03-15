using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LastBreakthrought.Other
{
    public class MenuButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float DURATIOIN = 0.2f;
        private const float SCALED_VALUE = 1.1f;
        private const float UNSCALED_VALUE = 1f;

        [SerializeField] private RectTransform _selectedArrowRoot;
        public Ease Ease;

        private void OnEnable() => 
            _selectedArrowRoot.gameObject.SetActive(false);

        public void OnPointerEnter(PointerEventData eventData)
        {
            _selectedArrowRoot.gameObject.SetActive(true);

            transform.DOScale(SCALED_VALUE, DURATIOIN)
                .SetEase(Ease)
                .Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _selectedArrowRoot.gameObject.SetActive(false);

            transform.DOScale(UNSCALED_VALUE, DURATIOIN)
                .SetEase(Ease)
                .Play();
        }
    }
}
