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
        [SerializeField] private Button _followButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _doWorkButton;

        [Header("ButtonColor:")]
        [SerializeField] private Color32 _diselectedColor;
        [SerializeField] private Color32 _selectedColor;

        private Button _currentSeletedButton;
        private float _maxBattaryCapacity;

        public void Init(RobotConfigSO RobotData, Action followAction, Action goHomeAction, Action doWorkActine)
        {
            _maxBattaryCapacity = RobotData.MaxBattaryCapacity;
            _battarySlider.maxValue = _maxBattaryCapacity;
            _battarySlider.value = _maxBattaryCapacity;

            _followButton.onClick.AddListener(() => {
                followAction?.Invoke();
                SetSelectedButton(_followButton);
            });
            _homeButton.onClick.AddListener(() => {
                goHomeAction?.Invoke();
                SetSelectedButton(_homeButton);
            });
            _doWorkButton.onClick.AddListener(() => {
                doWorkActine?.Invoke();
                SetSelectedButton(_doWorkButton);
            });
        }

        public void SetSliderValue(float value) => 
            _battarySlider.value = value;

        private void SetSelectedButton(Button button)
        {
            if (_currentSeletedButton != null)
                _currentSeletedButton.image.color = _diselectedColor;

            _currentSeletedButton = button;
            _currentSeletedButton.image.color = _selectedColor;
        }
    }
}
