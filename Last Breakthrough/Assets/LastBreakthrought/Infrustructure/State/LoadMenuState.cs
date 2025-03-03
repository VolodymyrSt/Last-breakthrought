using LastBreakthrought.Logic;
using System;

namespace LastBreakthrought.Infrustructure.State
{
    public class LoadMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly SceneLoader _sceneLoader;

        public LoadMenuState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _loadingCurtain.Procced(); 
            _sceneLoader.Load(SceneName.Menu, OnLoaded);
        }

        private void OnLoaded()
        {
            
        }

        public void Exit()
        {

        }
    }
}
