using LastBreakthrought.Configs.Robot;
using LastBreakthrought.Factory;
using LastBreakthrought.Infrustructure.AssetManagment;
using LastBreakthrought.NPC.Robot;
using System;
using UnityEngine;

namespace LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel.RobotControls.Factory
{
    public class RobotMinerControlUIFactory : BaseFactory<RobotControlHandlerUI>
    {
        public RobotMinerControlUIFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public override RobotControlHandlerUI Create(Vector3 at, Transform parent) =>
            AssetProvider.Instantiate<RobotControlHandlerUI>(AssetPath.RobotMinerControlPath, at, parent);

        public RobotControlHandlerUI Create(RectTransform parent, RobotConfigSO robotData, RobotBattary battary,
            Action followAction, Action goHomeAction, Action mineAction)
        {
            var robotControl = Create(Vector3.zero, parent);
            robotControl.Init(robotData, battary, followAction, goHomeAction, mineAction);
            return robotControl;
        }
    }
}
