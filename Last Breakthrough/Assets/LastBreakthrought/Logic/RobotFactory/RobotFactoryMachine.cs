using LastBreakthrought.Infrustructure.Services.Massage;
using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.Logic.Mechanisms;
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

        private RobotMinerFactory _robotMinerFactory;
        private RobotTransporterFactory _robotTransporterFactory;
        private RobotMenuPanelHandler _robotMenuPanelHandler;
        private MechanismsContainer _mechanismsContainer;
        private InventoryMenuPanelHandler _inventory;
        private IMassageHandlerService _massageHandler;
        private RequireMechanismsProvider _requireMechanismsProvider;

        private int _currentMinersCount = 0;
        private int _currentTransportersCount = 0;

        [Inject]
        private void Construct(RobotMinerFactory robotFactory, RobotTransporterFactory robotTransporterFactory,
            RobotMenuPanelHandler robotMenuPanelHandler, MechanismsContainer mechanismsContainer
            , InventoryMenuPanelHandler detailInventory, IMassageHandlerService massage, RequireMechanismsProvider requireMechanismsProvider)
        {
            _robotMinerFactory = robotFactory;
            _robotTransporterFactory = robotTransporterFactory;
            _robotMenuPanelHandler = robotMenuPanelHandler;
            _mechanismsContainer = mechanismsContainer;
            _inventory = detailInventory;
            _massageHandler = massage;
            _requireMechanismsProvider = requireMechanismsProvider;
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
                if (_mechanismsContainer.IsSearchedMechanismsAllFound(GetMechanismsToCreateMiner()))
                {
                    _mechanismsContainer.GiveMechanisms(GetMechanismsToCreateMiner());
                    _inventory.UpdateInventoryMechanisms(GetMechanismsToCreateMiner());
                    CreateMiner();
                }
                else
                    _massageHandler.ShowMassage("You cann`t create because you don`t have right details");
            }
            else
                _massageHandler.ShowMassage("You can only have three miners");
        }

        public void CreateRobotTransporter()
        {
            if (_currentTransportersCount < MAX_TRANSPORTERS_COUNT)
            {
                if (_mechanismsContainer.IsSearchedMechanismsAllFound(GetMechanismsToCreateTransporter()))
                {
                    _mechanismsContainer.GiveMechanisms(GetMechanismsToCreateTransporter());
                    _inventory.UpdateInventoryMechanisms(GetMechanismsToCreateTransporter());
                    CreateTransporter();
                }
                else
                    _massageHandler.ShowMassage("You cann`t create because you don`t have right details");
            }
            else
                _massageHandler.ShowMassage("You can only have three transporters");
        }

        public List<MechanismEntity> GetMechanismsToCreateMiner() =>
            _requireMechanismsProvider.Holder.CreateRobotMiner.GetRequiredShipDetails();

        public List<MechanismEntity> GetMechanismsToCreateTransporter() =>
            _requireMechanismsProvider.Holder.CreateRobotTransporter.GetRequiredShipDetails();

        private void CreateMiner()
        {
            var robotMiner = _robotMinerFactory.CreateRobot(_robotSpawnPoint.position, _robotSpawnPoint,
                                    _robotWanderingZone, _chargingPlaces);

            _robotMenuPanelHandler.AddRobotMinerControlUI(robotMiner.GetRobotData(), 
                robotMiner.GetRobotBattary(), robotMiner.GetRobotHealth(), robotMiner.SetFollowingPlayerState
                , robotMiner.SetWanderingState, mineAction: robotMiner.DoWork);

            _currentMinersCount++;
        }

        private void CreateTransporter()
        {
            var robotTransporter = _robotTransporterFactory.CreateRobot(_robotSpawnPoint.position,
                                    _robotSpawnPoint, _robotWanderingZone, _chargingPlaces);

            _robotMenuPanelHandler.AddRobotTransporterControlUI(robotTransporter.GetRobotData(),
                robotTransporter.GetRobotBattary(), robotTransporter.GetRobotHealth(), robotTransporter.SetFollowingPlayerState,
                robotTransporter.SetWanderingState, transportAction: robotTransporter.DoWork);

            _currentTransportersCount++;
        }
    }
}
