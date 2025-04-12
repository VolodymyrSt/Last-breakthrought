using Assets.LastBreakthrought.UI.Inventory.ShipDetail;
using DG.Tweening;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.UI.Inventory.Mechanism;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.UI.Inventory
{
    public class InventoryMenuPanelView : MonoBehaviour
    {
        private const float ANIMATION_DURATION = 0.2f;

        [Header("UI")]
        [SerializeField] private RectTransform _root;
        [SerializeField] private RectTransform _detailContainer;
        [SerializeField] private RectTransform _mechanismContainer;
        [SerializeField] private Button _openClosedInventoryMenuButton;

        [field: SerializeField] public DetailContainerUI DetailsContainerUI { get; private set; }
        [field: SerializeField] public MechanismsContainerUI MechanismsContainer { get; private set; }

        private bool _isMenuOpen = false;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus) =>
            _eventBus = eventBus;

        public void Init()
        {
            _openClosedInventoryMenuButton.onClick.AddListener(() => PerformOpenAndClose());
            _eventBus.SubscribeEvent<OnRobotMenuOpenedSignal>(CheckIfNeedToBeClose);
            _eventBus.SubscribeEvent<OnMapMenuOpenedSignal>(CheckIfNeedToBeClose);

            _root.localScale = Vector3.zero;
            _root.gameObject.SetActive(false);
        }

        public RectTransform GetDetailContainer() => _detailContainer;
        public RectTransform GetMechanismContainer() => _mechanismContainer;

        public void Open()
        {
            _eventBus.Invoke(new OnInventoryMenuOpenedSignal());

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
            foreach (Transform item in _detailContainer)
                item.localScale = _isMenuOpen ? Vector3.one : Vector3.zero;

            foreach (Transform item in _mechanismContainer)
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

        private void CheckIfNeedToBeClose(OnRobotMenuOpenedSignal signal)
        {
            if (_isMenuOpen)
                Close();
        }
        private void CheckIfNeedToBeClose(OnMapMenuOpenedSignal signal)
        {
            if (_isMenuOpen)
                Close();
        }
    }
}
