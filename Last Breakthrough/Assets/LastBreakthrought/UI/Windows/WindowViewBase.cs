using DG.Tweening;
using UnityEngine;

namespace LastBreakthrought.UI.Windows
{
    public abstract class WindowViewBase : MonoBehaviour
    {
        private bool _isHidden;

        private void Start()
        {
            Initialize();
            Hide();
        }

        private void OnDestroy() => Dispose();

        public abstract void Initialize();
        public abstract void Dispose();

        public virtual void ShowView()
        {
            if (!_isHidden) return;

            var duraction = 0.2f;
            gameObject.SetActive(true);
            transform.DOScale(1f, duraction)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => _isHidden = false);
        }

        public virtual void HideView()
        {
            if (_isHidden) return;

            var duraction = 0.2f;
            gameObject.SetActive(true);
            transform.DOScale(0f, duraction)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => {
                    gameObject.SetActive(false);
                    _isHidden = true;
                });
        }

        private void Hide()
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
            _isHidden = true;
        }
    }
}
