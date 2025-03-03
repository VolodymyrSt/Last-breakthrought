using LastBreakthrought.Logic;
using System;
using System.Collections.Generic;

namespace LastBreakthrought.Infrustructure.State
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentStates;

        public GameStateMachine(LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, loadingCurtain, sceneLoader),
                [typeof(LoadProgressState)] = new LoadProgressState(this),
                [typeof(LoadGameState)] = new LoadGameState(this)
            };
        }

        public void Enter<TState>() where TState : IState 
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private IState ChangeState<TState>()
        {
            _currentStates?.Exit();

            var state = _states[typeof(TState)];
            _currentStates = state;
            return state;
        }
    }
}
