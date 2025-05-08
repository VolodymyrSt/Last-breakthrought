using LastBreakthrought.Infrustructure.Services.EventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LastBreakthrought.Infrustructure.UI
{
    public abstract class MainMenuButton : MonoBehaviour 
    {
        [SerializeField] protected Button Button;

        private void Awake() => OnAwake();
        public abstract void OnAwake();
    }
}
