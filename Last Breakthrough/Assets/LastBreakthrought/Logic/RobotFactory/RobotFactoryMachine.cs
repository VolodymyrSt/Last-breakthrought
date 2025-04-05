using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.NPC.Robot.Factory;
using LastBreakthrought.UI.Inventory;
using LastBreakthrought.UI.NPC.Robot.RobotsMenuPanel;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.RobotFactory
{
    public class RobotFactoryMachine : MonoBehaviour
    {
        private const int MAX_MINERS_COUNT = 3;
        private const int MAX_TRANSPORTERS_COUNT = 3;

        [Header("Base:")]
        [SerializeField] private Transform _robotSpawnPoint;
        [SerializeField] private BoxCollider _robotWanderingZone;
        [SerializeField] private List<RobotChargingPlace> _chargingPlaces;

        [Header("DetailsForRobotsCreating:")]
        [SerializeField] private RequireDetailsForCreating _neededDetailsToCreateMiner;
        [SerializeField] private RequireDetailsForCreating _neededDetailsToCreateTransporter;

        private RobotMinerFactory _robotMinerFactory;
        private RobotTransporterFactory _robotTransporterFactory;
        private RobotMenuPanelHandler _robotMenuPanelHandler;
        private DetailsContainer _detailsContainer;
        private DetailInventoryMenuPanelHandler _detailInventory;

        private int _currentMinersCount = 0;
        private int _currentTransportersCount = 0;

        [Inject]
        private void Construct(RobotMinerFactory robotFactory, RobotTransporterFactory robotTransporterFactory,
            RobotMenuPanelHandler robotMenuPanelHandler, DetailsContainer detailsContainer, DetailInventoryMenuPanelHandler detailInventory)
        {
            _robotMinerFactory = robotFactory;
            _robotTransporterFactory = robotTransporterFactory;
            _robotMenuPanelHandler = robotMenuPanelHandler;
            _detailsContainer = detailsContainer;
            _detailInventory = detailInventory;
        }

        public void CreateStartedRobotsAtTheBeginning()
        {
            CreateMiner();
            CreateTransporter();
        }

        public void CreateRobotMiner()
        {
            if (_currentMinersCount < MAX_MINERS_COUNT)
            {
                if (_detailsContainer.IsSearchedDetailsAllFound(GetDetailsToCreateMiner()))
                {
                    _detailsContainer.GiveDetails(GetDetailsToCreateMiner());
                    _detailInventory.UpdateInventoryDetails(GetDetailsToCreateMiner());
                    CreateMiner();
                }
                else
                {
                    //show massage
                }
            }
            else
            {
                //show massage
            }
        }

        public void CreateRobotTransporter()
        {
            if (_currentTransportersCount < MAX_TRANSPORTERS_COUNT)
            {
                if (_detailsContainer.IsSearchedDetailsAllFound(GetDetailsToCreateTransporter()))
                {
                    CreateTransporter();
                }
                else
                {
                    //show massage
                }
            }
            else
            {
                //show massage
            }
        }

        public List<ShipDetailEntity> GetDetailsToCreateMiner() =>
            _neededDetailsToCreateMiner.GetNeededShipDetails();

        public List<ShipDetailEntity> GetDetailsToCreateTransporter() =>
            _neededDetailsToCreateTransporter.GetNeededShipDetails();

        private void CreateMiner()
        {
            var robotMiner = _robotMinerFactory.CreateRobot(_robotSpawnPoint.position, _robotSpawnPoint,
                                    _robotWanderingZone, _chargingPlaces);

            _robotMenuPanelHandler.AddRobotMinerControlUI(robotMiner.GetRobotData(),
                robotMiner.GetRobotBattary(), robotMiner.SetFollowingPlayerState
                , robotMiner.SetWanderingState, mineAction: robotMiner.DoWork);

            _currentMinersCount++;
        }

        private void CreateTransporter()
        {
            var robotTransporter = _robotTransporterFactory.CreateRobot(_robotSpawnPoint.position,
                                    _robotSpawnPoint, _robotWanderingZone, _chargingPlaces);

            _robotMenuPanelHandler.AddRobotTransporterControlUI(robotTransporter.GetRobotData(),
                robotTransporter.GetRobotBattary(), robotTransporter.SetFollowingPlayerState,
                robotTransporter.SetWanderingState, transportAction: robotTransporter.DoWork);

            _currentTransportersCount++;
        }
    }
}
