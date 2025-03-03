namespace LastBreakthrought.Infrustructure.State
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
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
