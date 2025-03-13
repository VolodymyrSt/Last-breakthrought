using UnityEngine;

namespace LastBreakthrought.UI.SlotItem
{
    public abstract class Item : MonoBehaviour, IItem
    {
        public abstract void Select();

        public abstract void UnSelect();
    }
}
