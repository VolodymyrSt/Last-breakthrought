using LastBreakthrought.Configs.Enemy;
using LastBreakthrought.Configs.Game;
using LastBreakthrought.Configs.Player;
using LastBreakthrought.Infrustructure.AssetManagment;
using LastBreakthrought.Infrustructure.Services.Input;
using LastBreakthrought.Logic;
using LastBreakthrought.Other;
using LastBreakthrought.Util;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Infrustructure.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        [Header("Configs")]
        [SerializeField] private PlayerConfigSO _playerConfigSO;
        [SerializeField] private GameConfigSO _gameConfigSO;
        [SerializeField] private EnemyConfigHolderSO _enemyConfigHolderSO;

        public override void InstallBindings()
        {
            BindLoadingCurtain();
            BindCoroutineRunner();

            BindInput();
            BindConfigs();

            BindSceneLoader();

            Container.Bind<Game>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.Bind<GameConfigSO>().FromInstance(_gameConfigSO).AsSingle();
            Container.Bind<PlayerConfigSO>().FromInstance(_playerConfigSO).AsSingle();
            Container.Bind<EnemyConfigHolderSO>().FromInstance(_enemyConfigHolderSO).AsSingle();
        }

        private void BindSceneLoader() => 
            Container.Bind<SceneLoader>().AsSingle();

        private void BindCoroutineRunner()
        {
            var coroutineRunner = Container.InstantiatePrefabForComponent<CoroutineRunner>(_coroutineRunner);
            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(coroutineRunner);
        }

        private void BindLoadingCurtain()
        {
            var curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(_curtainPrefab);
            Container.Bind<LoadingCurtain>().FromInstance(curtain);
        }

        private void BindInput()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
                Container.Bind<IInputService>().To<MobileInput>().AsSingle();
            else
                Container.Bind<IInputService>().To<StandeloneInput>().AsSingle();
        }
    }
}

