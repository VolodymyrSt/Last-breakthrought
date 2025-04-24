using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LastBreakthrought.Other
{
    public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float DURATIOIN = 0.2f;
        private const float SCALED_VALUE = 1.1f;
        private const float UNSCALED_VALUE = 1f;

        [SerializeField] private Ease _ease;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(SCALED_VALUE, DURATIOIN)
                .SetEase(_ease)
                .Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            transform.DOScale(UNSCALED_VALUE, DURATIOIN)
                .SetEase(_ease)
                .Play();
        }
    }
}
