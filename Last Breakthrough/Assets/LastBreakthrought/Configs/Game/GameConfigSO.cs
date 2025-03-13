using UnityEngine;

namespace LastBreakthrought.Configs.Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game")]
    public class GameConfigSO : ScriptableObject
    {
        [Header("Timer")]
        [field: SerializeField, Range(0, 4)] public int StartedDay { get; private set; }
        [field: SerializeField, Range(0, 24)] public int StartedMinute { get; private set; }
        [field: SerializeField, Range(0, 60)] public int StartedSecond { get; private set; }


        [field: SerializeField, Range(0f, 10f)] public float OxygenIncreasingIndex { get; private set; }
    }
}
