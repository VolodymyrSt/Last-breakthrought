using UnityEngine;

namespace LastBreakthrought.NPC.Enemy
{
    public interface IEnemy
    {
        void OnSpawned(BoxCollider wanderingZone, string id);
    }
}
