using DG.Tweening;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Infrustructure.Services.EventBus;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace LastBreakthrought.UI.Map
{
    public class MapMenuPanelView : MonoBehaviour
    {
        private const float ANIMATION_DURATION = 0.2f;

        [Header("UI")]
        [SerializeField] private RectTransform _markersContent;
        [SerializeField] private RectTransform _root;
        [SerializeField] private Button _openClosedMapMenuButton;

        private bool _isMenuOpen = false;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus) =>
            _eventBus = eventBus;

        public void Init()
        {
            _openClosedMapMenuButton.onClick.AddListener(() => PerformOpenAndClose());
            _eventBus.SubscribeEvent<OnInventoryMenuOpenedSignal>(CheckIfNeedToBeClose);
            _eventBus.SubscribeEvent<OnRobotMenuOpenedSignal>(CheckIfNeedToBeClose);

            _root.localScale = Vector3.zero;
            _root.gameObject.SetActive(false);
        }

        public void OnNewItemAdded(Transform newItem) =>
            newItem.localScale = _isMenuOpen ? Vector3.one : Vector3.zero;

        public RectTransform GetMarkerContainer() => _markersContent;

        public void Open()
        {
            _eventBus.Invoke(new OnMapMenuOpenedSignal());

            _root.gameObject.SetActive(true);
            _root.DOScale(1f, ANIMATION_DURATION)
                .SetEase(Ease.Linear)
                .Play().OnComplete(() =>
                {
                    _isMenuOpen = true;
                    UpdateChildrenScale();
                });
        }

        public void UpdateChildrenScale()
        {
            foreach (Transform item in _markersContent)
                item.localScale = _isMenuOpen ? Vector3.one : Vector3.zero;
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

        private void CheckIfNeedToBeClose(OnInventoryMenuOpenedSignal signal)
        {
            if (_isMenuOpen)
                Close();
        }

        private void CheckIfNeedToBeClose(OnRobotMenuOpenedSignal signal)
        {
            if (_isMenuOpen)
                Close();
        }
    }
}
