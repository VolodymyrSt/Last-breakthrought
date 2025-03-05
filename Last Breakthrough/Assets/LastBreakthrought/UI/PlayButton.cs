using LastBreakthrought.Infrustructure.Services;
using LastBreakthrought.Infrustructure.State;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.Infrustructure.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        private Game _game;

        [Inject]
        private void Construct(Game game) =>
            _game = game;

        private void Awake()
        {
            _playButton.onClick.AddListener(() => EnterLoadGameState());
        }

        private void EnterLoadGameState()
        {
            _game.StateMachine.Enter<LoadGameplayState>();
        }
    }
}
