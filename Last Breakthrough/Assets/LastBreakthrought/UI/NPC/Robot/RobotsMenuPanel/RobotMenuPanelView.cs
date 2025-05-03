using DG.Tweening;
using LastBreakthrought.Infrustructure.Services.AudioService;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Logic.Camera;
using LastBreakthrought.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel
{
    public class RobotMenuPanelView : MonoBehaviour
    {
        private const float ANIMATION_DURATION = 0.2f;

        [Header("UI")]
        [SerializeField] private RectTransform _root;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Button _openClosedRobotsMenuButton;

        private IEventBus _eventBus;
        private IAudioService _audioService;
        private FollowCamera _followCamera;

        private bool _isMenuOpen = false;
        private bool _isTutorialEnded = false;

        [Inject]
        private void Construct(IEventBus eventBus, IAudioService audioService, FollowCamera followCamera)
        {
            _eventBus = eventBus;
            _audioService = audioService;
            _followCamera = followCamera;
        }

        public void Init()
        {
            _openClosedRobotsMenuButton.onClick.AddListener(() => PerformOpenAndClose());
            _eventBus.SubscribeEvent<OnInventoryMenuOpenedSignal>(CheckIfNeedToBeClose);
            _eventBus.SubscribeEvent<OnMapMenuOpenedSignal>(CheckIfNeedToBeClose);

            _eventBus.SubscribeEvent((OnTutorialEndedSignal signal) => _isTutorialEnded = true);

            _root.localScale = Vector3.zero;
            _root.gameObject.SetActive(false);
        }

        public RectTransform GetContainer() => _content;

        public void OpenPanel()
        {
            _eventBus.Invoke(new OnRobotMenuOpenedSignal());

            _root.gameObject.SetActive(true);
            _root.DOScale(1f, ANIMATION_DURATION)
                .SetEase(Ease.Linear)
                .Play().OnComplete(() =>
                {
                    _isMenuOpen = true;
                    UpdateChildrenScale();
                });
        }

        public void OnNewItemAdded(Transform newItem) =>
            newItem.localScale = _isMenuOpen ? Vector3.one : Vector3.zero;

        public void UpdateChildrenScale()
        {
            foreach (Transform item in _content)
                item.localScale = _isMenuOpen? Vector3.one : Vector3.zero;
        }

        private void Open()
        {
            _eventBus.Invoke(new OnRobotMenuOpenedSignal());
            _audioService.PlayOnObject(Configs.Sound.SoundType.PanelOpen, _followCamera);

            _root.gameObject.SetActive(true);
            _root.DOScale(1f, ANIMATION_DURATION)
                .SetEase(Ease.Linear)
                .Play().OnComplete(() =>
                {
                    _isMenuOpen = true;
                    UpdateChildrenScale();
                });
        }

        private void PerformOpenAndClose()
        {
            if (!_isTutorialEnded) 
                return;
            else
            {
                if (_isMenuOpen)
                    Close();
                else
                    Open();
            }
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

        private void CheckIfNeedToBeClose(OnMapMenuOpenedSignal signal)
        {
            if (_isMenuOpen)
                Close();
        }
    }
}
