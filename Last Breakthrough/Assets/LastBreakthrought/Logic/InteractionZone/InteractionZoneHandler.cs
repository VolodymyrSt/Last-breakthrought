using LastBreakthrought.UI.Windows;
using UnityEngine;

namespace LastBreakthrought.Logic.InteractionZone
{
    public class InteractionZoneHandler : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private InterationZoneView _interationZoneView;
        [SerializeField] private WindowHandlerBase _windowHandler;

        private void OnEnable()
        {
            _interationZoneView.OnPlayerEnter += ShowPopup;
            _interationZoneView.OnPlayerExit += HidePopup;

            _interationZoneView.HideOnInit();
        }

        public void Disactivate()
        {
            gameObject.SetActive(false);
            HidePopup();
        }

        public void Activate() => gameObject.SetActive(true);

        private void HidePopup() =>  _windowHandler.DeactivateWindow();
        private void ShowPopup() =>  _windowHandler.ActivateWindow();

        private void OnDisable()
        {
            _interationZoneView.OnPlayerEnter -= ShowPopup;
            _interationZoneView.OnPlayerExit -= HidePopup;
        }
    }
}
