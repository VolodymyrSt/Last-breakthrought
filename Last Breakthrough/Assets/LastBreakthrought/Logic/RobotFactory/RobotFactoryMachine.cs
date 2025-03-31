using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.NPC.Robot;
using LastBreakthrought.NPC.Robot.Factory;
using LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.RobotFactory
{
    public class RobotFactoryMachine : MonoBehaviour
    {
        private RobotMinerFactory _robotMinerFactory;
        private RobotTransporterFactory _robotTransporterFactory;
        private RobotMenuPanelHandler _robotMenuPanelHandler;

        [SerializeField] private Transform _robotSpawnPoint;
        [SerializeField] private BoxCollider _robotWanderingZone;
        [SerializeField] private List<RobotChargingPlace> _chargingPlaces;

        [Inject]
        private void Construct(RobotMinerFactory robotFactory, RobotTransporterFactory robotTransporterFactory, RobotMenuPanelHandler robotMenuPanelHandler)
        {
            _robotMinerFactory = robotFactory;
            _robotTransporterFactory = robotTransporterFactory;
            _robotMenuPanelHandler = robotMenuPanelHandler;
        }

        public void CreateRobotMiner()
        {
            var robotMiner = _robotMinerFactory.CreateRobot(_robotSpawnPoint.position, _robotSpawnPoint,
                _robotWanderingZone, _chargingPlaces);

            var robot = robotMiner as RobotMiner;

            _robotMenuPanelHandler.AddRobotMinerControlUI(robotMiner.GetRobotData(),
                robotMiner.GetRobotBattary(), robotMiner.SetFollowingPlayerState
                , robotMiner.SetWanderingState, mineAction: robot.SetMiningState);
        }

        public void CreateRobotTransporter()
        {
            var robotTransporter = _robotTransporterFactory.CreateRobot(_robotSpawnPoint.position,
                _robotSpawnPoint, _robotWanderingZone, _chargingPlaces);

            _robotMenuPanelHandler.AddRobotTransporterControlUI(robotTransporter.GetRobotData(),
                robotTransporter.GetRobotBattary(), robotTransporter.SetFollowingPlayerState,
                robotTransporter.SetWanderingState, transportAction: robotTransporter.DoNothing);
        }
    }
}
