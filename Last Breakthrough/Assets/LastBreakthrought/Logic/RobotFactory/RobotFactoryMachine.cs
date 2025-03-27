using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.NPC.Robot.Factory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.RobotFactory
{
    public class RobotFactoryMachine : MonoBehaviour
    {
        private RobotMinerFactory _robotMinerFactory;
        private RobotTransporterFactory _robotTransporterFactory;

        [SerializeField] private Transform _robotSpawnPoint;
        [SerializeField] private BoxCollider _robotWanderingZone;
        [SerializeField] private List<RobotChargingPlace> _chargingPlaces;

        [Inject]
        private void Construct(RobotMinerFactory robotFactory, RobotTransporterFactory robotTransporterFactory)
        {
            _robotMinerFactory = robotFactory;
            _robotTransporterFactory = robotTransporterFactory;
        }

        public void CreateRobotMiner() =>
            _robotMinerFactory.CreateRobot(_robotSpawnPoint.position, _robotSpawnPoint, _robotWanderingZone, _chargingPlaces);
        public void CreateRobotTransporter() =>
            _robotTransporterFactory.CreateRobot(_robotSpawnPoint.position, _robotSpawnPoint, _robotWanderingZone, _chargingPlaces);
    }
}
