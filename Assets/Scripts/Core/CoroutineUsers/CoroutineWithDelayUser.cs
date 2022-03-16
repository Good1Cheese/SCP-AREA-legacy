using System.Collections;
using UnityEngine;

public abstract class CoroutineWithDelayUser : CoroutineUser 
{
    [SerializeField] private float _delayBeforeCoroutine;

    protected WaitForSeconds _timeoutBeforeCoroutine;

    public float DelayBeforeCoroutine { get => _delayBeforeCoroutine; }

    protected new void Start()
    {
        base.Start();
        _timeoutBeforeCoroutine = new WaitForSeconds(DelayBeforeCoroutine);
    }

    public void StartWithoutInterrupt(WaitForSeconds timeout)
    {
        if (IsCoroutineGoing) { return; }

        StartCoroutine(Coroutine(timeout));
    }

    public override void StartWithoutInterrupt() => StartWithoutInterrupt(_timeoutBeforeCoroutine);

    protected IEnumerator Coroutine(WaitForSeconds timeout)
    {
        yield return timeout;

        yield return Coroutine();

        IsCoroutineGoing = false;
    }
}