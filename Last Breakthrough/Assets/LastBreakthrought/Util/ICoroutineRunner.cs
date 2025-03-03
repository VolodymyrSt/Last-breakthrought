using System.Collections;
using UnityEngine;

namespace LastBreakthrought.Util 
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}

