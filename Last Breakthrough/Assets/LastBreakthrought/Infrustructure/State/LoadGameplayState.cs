using LastBreakthrought.Logic;
using System;
using UnityEngine;

namespace LastBreakthrought.Infrustructure.State
{
    public class LoadGameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly SceneLoader _sceneLoader;

        public LoadGameplayState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            Debug.Log("LoadGameplayState enterd");
            _loadingCurtain.Procced();
            _sceneLoader.Load(SceneName.Gameplay, null);
        }

        public void Exit()
        {

        }
    }
}
