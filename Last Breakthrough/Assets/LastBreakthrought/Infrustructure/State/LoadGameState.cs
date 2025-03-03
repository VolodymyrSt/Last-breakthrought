namespace LastBreakthrought.Infrustructure.State
{
    public class LoadGameState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public LoadGameState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}
