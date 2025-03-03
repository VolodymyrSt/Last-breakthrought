using LastBreakthrought.Infrustructure.State;
using LastBreakthrought.Util;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Infrustructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        [Inject]
        private void Construct(Game game) =>
            _game = game;

        private void Awake()
        {
            _game.StateMachine.Enter<BootStrapState>();

            DontDestroyOnLoad(gameObject);
        }
    }
}
