using Zenject;
using UnityEngine;
using LastBreakthrought.Logic.Camera;
using LastBreakthrought.Player;
using SimpleInputNamespace;

namespace LastBreakthrought.Infrustructure.Installers 
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMovement _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        [SerializeField] private FollowCamera _cameraPrefab;

        [SerializeField] private GameObject _gameplayHubPrefab;
        [SerializeField] private GameObject _joyStickPrefab;

        private GameObject _gameplayHub;

        public override void InstallBindings()
        {
            BindPlayer();
            BindCamera();

            BindGamePlayHub();
            BindJoyStick();
        }

        private void BindJoyStick()
        {
            //if (SystemInfo.deviceType == DeviceType.Handheld)
                Container.InstantiatePrefab(_joyStickPrefab, _gameplayHub.transform);
        }

        private void BindGamePlayHub()
        {
            _gameplayHub = Container.InstantiatePrefab(_gameplayHubPrefab);
        }

        private void BindCamera()
        {
            var camera = Container.InstantiatePrefabForComponent<FollowCamera>(_cameraPrefab);
            Container.Bind<FollowCamera>().FromInstance(camera);
        }

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<PlayerMovement>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);
            Container.Bind<PlayerMovement>().FromInstance(player);
        }
    }
}

