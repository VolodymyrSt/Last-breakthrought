using LastBreakthrought.Configs.Robot;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel.RobotControls
{
    public class RobotControlViewUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider _battarySlider;
        [SerializeField] private Button _followButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _doWorkButton;

        private float _maxBattaryCapacity;

        public void Init(RobotConfigSO RobotData, Action followAction, Action goHomeAction, Action doWorkActine)
        {
            _maxBattaryCapacity = RobotData.MaxBattaryCapacity;
            _battarySlider.maxValue = _maxBattaryCapacity;
            _battarySlider.value = _maxBattaryCapacity;

            _followButton.onClick.AddListener(() => followAction?.Invoke());
            _homeButton.onClick.AddListener(() => goHomeAction?.Invoke());
            _doWorkButton.onClick.AddListener(() => doWorkActine?.Invoke());
        }

        public void SetSliderValue(float value) => 
            _battarySlider.value = value;
    }
}
