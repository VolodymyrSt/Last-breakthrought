using UnityEngine;

namespace LastBreakthrought.CrashedShip
{
    public interface ICrashedShip
    {
        Vector3 GetPosition();
        void OnInitialized();
        void DestroySelf();
    }
}
