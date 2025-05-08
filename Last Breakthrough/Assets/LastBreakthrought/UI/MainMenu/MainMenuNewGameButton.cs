using LastBreakthrought.Infrustructure.Services;
using LastBreakthrought.Infrustructure.State;
using System;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.Infrustructure.UI
{
    public class MainMenuNewGameButton : MainMenuButton
    {
        private Game _game;

        [Inject]
        private void Construct(Game game) =>
            _game = game;

        public override void OnAwake() =>
            Button.onClick.AddListener(() => EnterLoadGameState());

        private void EnterLoadGameState() =>
            _game.StateMachine.Enter<LoadGameplayState>();
    }
}
