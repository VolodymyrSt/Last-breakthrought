using LastBreakthrought.Logic;
using LastBreakthrought.Util;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Infrustructure.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private ICoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            BindLoadingCurtain();

            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<Game>().AsSingle();
        }

        private void BindLoadingCurtain()
        {
            var curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(_curtainPrefab);
            Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromInstance(curtain);
        }
    }
}

