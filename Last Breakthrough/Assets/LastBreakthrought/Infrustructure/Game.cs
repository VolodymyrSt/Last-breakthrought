using LastBreakthrought.Infrustructure.State;
using LastBreakthrought.Logic;
using Zenject;

namespace LastBreakthrought.Infrustructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(LoadingCurtain loadingCurtain, SceneLoader sceneLoader, DiContainer container)
        {
            StateMachine = new GameStateMachine(loadingCurtain, sceneLoader, container);
        }
    }
}
