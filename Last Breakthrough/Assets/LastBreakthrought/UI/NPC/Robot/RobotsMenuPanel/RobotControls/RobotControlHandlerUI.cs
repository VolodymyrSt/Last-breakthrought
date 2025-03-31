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

        public void Init(RobotConfigSO robotData, RobotBattary battary, Action followAction, Action goHomeAction, Action doWorkAction)
        {
            _view.Init(robotData, followAction, goHomeAction, doWorkAction);
            _robotBattary = battary;
        }

        public void UpdateSlider() =>
            _view.SetSliderValue(_robotBattary.Capacity);
    }
}
