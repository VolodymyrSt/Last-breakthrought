using LastBreakthrought.Configs.Robot;
using LastBreakthrought.NPC.Robot;
using System;
using UnityEngine;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel.RobotControls
{
    public class RobotControlHandlerUI : MonoBehaviour
    {
        [SerializeField] private RobotControlViewUI _view;
        private RobotBattary _robotBattary;
        private RobotHealth _robotHealth;

        public void Init(RobotConfigSO robotData, RobotBattary battary, RobotHealth robotHealth, Action followAction, Action goHomeAction, Action doWorkAction)
        {
            _view.Init(robotData, followAction, goHomeAction, doWorkAction);
            _robotBattary = battary;
            _robotHealth = robotHealth;

            _robotHealth.OnHealthChanged += UpdateHealthSlider;
        }

        private void UpdateHealthSlider(float obj) => 
            _view.SetHealthSliderValue(obj);

        public void UpdateSlider() =>
            _view.SetBattarySliderValue(_robotBattary.Capacity);

        private void OnDestroy() => 
            _robotHealth.OnHealthChanged -= UpdateHealthSlider;
    }
}
