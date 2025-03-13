using UnityEngine;

namespace LastBreakthrought.UI.Windows
{
    public abstract class WindowViewBase : MonoBehaviour
    {
        private void Awake() => Initialize();
        private void Start() => HideView();
        private void OnDestroy() => Dispose();

        public abstract void Initialize();
        public abstract void Dispose();

        public virtual void ShowView() => gameObject.SetActive(true);
        public virtual void HideView() => gameObject.SetActive(false);
    }
}
