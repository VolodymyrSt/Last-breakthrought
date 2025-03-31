using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel
{
    public class RobotMenuPanelView : MonoBehaviour
    {
        private const float ANIMATION_DURATION = 0.2f;

        [Header("UI")]
        [SerializeField] private RectTransform _root;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Button _openClosedRobotsMenuButton;

        private bool _isMenuOpen = false;

        public void Init()
        {
            _openClosedRobotsMenuButton.onClick.AddListener(() => PerformOpenAndClose());
            _root.localScale = Vector3.zero;
            _root.gameObject.SetActive(false);
        }

        public RectTransform GetContainer() => _content;

        public void Open()
        {
            _root.gameObject.SetActive(true);
            _root.DOScale(1f, ANIMATION_DURATION)
                .SetEase(Ease.Linear)
                .Play().OnComplete(() => _isMenuOpen = true);

            //to prevent from 0 scale
            foreach (Transform item in _content)
                item.transform.localScale = Vector3.one;
        }

        private void PerformOpenAndClose()
        {
            if (_isMenuOpen)
                Close();
            else
                Open();
        }

        private void Close()
        {
            _root.DOScale(0f, ANIMATION_DURATION)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() =>
                {
                    _root.gameObject.SetActive(false);
                    _isMenuOpen = false;
                });
        }
    }
}
