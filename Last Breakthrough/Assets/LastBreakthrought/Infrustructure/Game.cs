using LastBreakthrought.Infrustructure.State;
using LastBreakthrought.Logic;

namespace LastBreakthrought.Infrustructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
        {
            StateMachine = new GameStateMachine(loadingCurtain, sceneLoader);
        }
    }
}
