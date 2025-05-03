using LastBreakthrought.Configs.Robot;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel.RobotControls
{
    public class RobotControlViewUI : MonoBehaviour
    {
        [Header("UI:")]
        [SerializeField] private Slider _battarySlider;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Button _followButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _doWorkButton;

        [Header("Button Colors:")]
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _pressedColor = Color.gray;
        [SerializeField] private Color _selectedColor = Color.blue;

        private Button _currentlySelectedButton;

        public void Init(RobotConfigSO RobotData, Action followAction, Action goHomeAction, Action doWorkAction)
        {
            var maxBattaryCapacity = RobotData.MaxBattaryCapacity;
            _battarySlider.maxValue = maxBattaryCapacity;
            _battarySlider.value = maxBattaryCapacity;

            var maxHealth = RobotData.MaxHealth;
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = maxHealth;

            _followButton.onClick.AddListener(() => {
                followAction?.Invoke();
                SetButtonSelected(_followButton);
            });

            _homeButton.onClick.AddListener(() => {
                goHomeAction?.Invoke();
                SetButtonSelected(_homeButton);
            });

            _doWorkButton.onClick.AddListener(() => {
                doWorkAction?.Invoke();
                SetButtonSelected(_doWorkButton);
            });

            ResetButtonColors();
        }

        private void SetButtonSelected(Button button)
        {
            if (_currentlySelectedButton != null)
            {
                if (_currentlySelectedButton == button)
                {
                    SetButtonColor(_currentlySelectedButton, _normalColor);
                    _currentlySelectedButton = null;
                    return;
                }
                SetButtonColor(_currentlySelectedButton, _normalColor);
            }

            _currentlySelectedButton = button;
            SetButtonColor(_currentlySelectedButton, _selectedColor);
        }

        private void ResetButtonColors()
        {
            SetButtonColor(_followButton, _normalColor);
            SetButtonColor(_homeButton, _normalColor);
            SetButtonColor(_doWorkButton, _normalColor);
        }

        private void SetButtonColor(Button button, Color color)
        {
            var colors = button.colors;
            colors.normalColor = color;
            colors.selectedColor = color;
            button.colors = colors;
        }
        public void SetBattarySliderValue(float value) => 
            _battarySlider.value = value;

        public void SetHealthSliderValue(float value) =>
            _healthSlider.value = value;
    }
}
