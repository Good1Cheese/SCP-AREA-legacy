using System;
using System.Collections;
using UnityEngine;

public abstract class CoroutineUser : MonoBehaviour
{
    private IEnumerator _coroutine;

    protected virtual IEnumerator Corotine => Coroutine();

    public bool IsCoroutineGoing { get; set; }
    public Action CoroutineStarted { get; set; }

    protected void Start()
    {
        _coroutine = Corotine;
    }

    public virtual void StartWithoutInterrupt()
    {
        if (IsCoroutineGoing) { return; }

        StartCoroutine(Corotine);
    }

    protected virtual new void StartCoroutine(IEnumerator enumerator)
    {
        IsCoroutineGoing = true;
        _coroutine = enumerator;

        base.StartCoroutine(_coroutine);
        CoroutineStarted?.Invoke();
    }

    public virtual void StopCoroutine()
    {
        IsCoroutineGoing = false;
        StopCoroutine(_coroutine);
    }

    protected abstract IEnumerator Coroutine();
}