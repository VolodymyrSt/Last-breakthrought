using UnityEngine;

namespace LastBreakthrought.UI.Windows 
{
    public abstract class WindowHandlerBase : MonoBehaviour
    {
        public abstract void ActivateWindow();
        public abstract void DeactivateWindow();
        public virtual void UseDevice() { }
    }
}
