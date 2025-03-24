using LastBreakthrought.Logic;
using LastBreakthrought.Other;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Infrustructure.State
{
    public class LoadGameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly SceneLoader _sceneLoader;
        private readonly DiContainer _container;

        public LoadGameplayState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, SceneLoader sceneLoader
            , DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _container = container;
        }

        public void Enter()
        {
            _loadingCurtain.Procced();
            _sceneLoader.Load(SceneName.Gameplay, OnLoaded);
        }

        private void OnLoaded()
        {
            var spawnersContainer = _container.Resolve<SpawnersContainer>();

            spawnersContainer.SpawnAllCrashedShips();

            _container.Resolve<NavMeshSurface>().BuildNavMesh();

            spawnersContainer.SpawnAllEnemies();
        }

        public void Exit(){}
    }
}
