using LastBreakthrought.Player;
using LastBreakthrought.UI.Windows;
using System;
using UnityEngine;
using Zenject;

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

            _interationZoneView.HideOnInit();
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
