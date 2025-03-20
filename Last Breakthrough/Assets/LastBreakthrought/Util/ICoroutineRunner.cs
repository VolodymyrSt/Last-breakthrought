using System.Collections;
using UnityEngine;

namespace LastBreakthrought.Util 
{
    public interface ICoroutineRunner
    {
        Coroutine PerformCoroutine(IEnumerator coroutine);
        Coroutine HandleStopCoroutine(Coroutine coroutine);
    }
}

