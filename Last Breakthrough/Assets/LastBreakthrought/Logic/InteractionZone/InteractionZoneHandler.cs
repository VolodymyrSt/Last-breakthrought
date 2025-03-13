using LastBreakthrought.UI.Windows;
using UnityEngine;

namespace LastBreakthrought.Logic.InteractionZone
{
    public class InteractionZoneHandler : MonoBehaviour
    {
        [SerializeField] private InterationZoneView _interationZoneView;
        [SerializeField] private WindowHandlerBase _windowHandler;

        private void OnEnable()
        {
            _interationZoneView.OnPlayerEnter += ShowPopup;
            _interationZoneView.OnPlayerExit += HidePopup;
        }

        private void HidePopup() => 
            _windowHandler.DeactivateWindow();

        private void ShowPopup() => 
            _windowHandler.ActivateWindow();

        private void OnDisable()
        {
            _interationZoneView.OnPlayerEnter -= ShowPopup;
            _interationZoneView.OnPlayerExit -= HidePopup;
        }
    }
}
