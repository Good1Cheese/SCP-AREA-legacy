using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static void ResetCoroutine(this MonoBehaviour script, IEnumerator method, IEnumerator enumerator)
    {
        script.StopCoroutine(method);
        method = enumerator;
    }
}
