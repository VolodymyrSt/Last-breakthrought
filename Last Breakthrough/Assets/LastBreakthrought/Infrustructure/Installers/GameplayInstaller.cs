using Zenject;
using UnityEngine;
using LastBreakthrought.Logic.Camera;
using LastBreakthrought.Player;
using LastBreakthrought.Other;
using LastBreakthrought.UI.Home;
using LastBreakthrought.UI.Timer;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.UI.PlayerStats;
using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.CrashedShip;
using LastBreakthrought.Infrustructure.AssetManagment;

namespace LastBreakthrought.Infrustructure.Installers 
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private PlayerHandler _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        [Header("Camera")]
        [SerializeField] private FollowCamera _cameraPrefab;

        [Header("UI")]
        [SerializeField] private GameObject _gameplayHubPrefab;
        [SerializeField] private GameObject _joyStickPrefab;

        [Header("HomePoint")]
        [SerializeField] private HomePoint _homePointPrefab;

        private GameObject _gameplayHub;

        public override void InstallBindings()
        {
            BindEventBus();
            BindConfigProviderService();
            BindAssetProvider();

            BindCrashedShipsContainer();
            BindCrashedShipFactory();

            BindPlayer();
            BindCamera();


            BindGamePlayHub();
            BindJoyStick();

            BindHomePoint();
            BindHomeDistanceInformer();

            BindTimer();

            BindPlayerStats();
        }

        private void BindAssetProvider() =>
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

        private void BindCrashedShipsContainer() => 
            Container.Bind<CrashedShipsContainer>().AsSingle();

        private void BindCrashedShipFactory() => 
            Container.Bind<CrashedShipFactory>().AsSingle();

        private void BindConfigProviderService() => 
            Container.Bind<IConfigProviderService>().To<ConfigProviderService>().AsSingle();

        private void BindPlayerStats()
        {
            var playerStatsView = _gameplayHub.GetComponentInChildren<PlayerStatsView>();
            Container.Bind<PlayerStatsView>().FromInstance(playerStatsView).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStatsModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStatsHandler>().AsSingle().NonLazy();
        }

        private void BindEventBus() => 
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();

        private void BindTimer()
        {
            var timerView = _gameplayHub.GetComponentInChildren<TimerView>();
            Container.Bind<TimerView>().FromInstance(timerView).AsSingle();

            Container.BindInterfacesAndSelfTo<TimerController>().AsSingle().NonLazy();
        }

        private void BindHomeDistanceInformer()
        {
            var homeDistanceView = _gameplayHub.GetComponentInChildren<HomeDistanceView>();
            Container.Bind<HomeDistanceView>().FromInstance(homeDistanceView).AsSingle();

            Container.BindInterfacesAndSelfTo<HomeDistanceCounter>().AsSingle().NonLazy();
        }

        private void BindJoyStick()
        {
            //if (SystemInfo.deviceType == DeviceType.Handheld)
                Container.InstantiatePrefab(_joyStickPrefab, _gameplayHub.transform);
        }

        private void BindGamePlayHub() => 
            _gameplayHub = Container.InstantiatePrefab(_gameplayHubPrefab);

        private void BindCamera()
        {
            var camera = Container.InstantiatePrefabForComponent<FollowCamera>(_cameraPrefab);
            Container.Bind<FollowCamera>().FromInstance(camera).AsSingle();
        }
        private void BindHomePoint() => 
            Container.Bind<HomePoint>().FromInstance(_homePointPrefab).AsSingle();

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<PlayerHandler>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);
            Container.Bind<PlayerHandler>().FromInstance(player).AsSingle();
        }
    }
}

