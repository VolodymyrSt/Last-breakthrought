using UnityEngine;

namespace LastBreakthrought.Other
{
    public class HomePoint : MonoBehaviour
    {
        public Vector3 GetPosition() =>
            transform.position;
    }

    public class TimeHandler
    {
        public void StopTime() => Time.timeScale = 0;
        public void ResetTime() => Time.timeScale = 1;
    }
}
