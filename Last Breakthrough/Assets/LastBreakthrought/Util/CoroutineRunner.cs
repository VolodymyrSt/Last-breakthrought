using System.Collections;
using UnityEngine;

namespace LastBreakthrought.Util
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine PerformCoroutine(IEnumerator coroutine) => 
            StartCoroutine(coroutine);
    }
}

