using LastBreakthrought.Infrustructure.Services;
using LastBreakthrought.Logic;
using LastBreakthrought.Util;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Infrustructure.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            BindLoadingCurtain();
            BindInput();
            BindCoroutineRunner();


            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<Game>().AsSingle();
        }

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

