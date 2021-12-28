using System.Collections;
using UnityEngine;

public abstract class CoroutineUser : MonoBehaviour
{
    protected WaitForSeconds _coroutineTimeout;
    private IEnumerator _actionCoroutine;
    protected virtual IEnumerator Action => Coroutine();

    public bool IsActionGoing { get; set; }

    protected void Start()
    {
        _actionCoroutine = Action;
    }

    public void StartActionWithInterrupt()
    {
        if (IsActionGoing) { return; }

        StartAction();
    }

    protected virtual void StartAction()
    {
        IsActionGoing = true;
        _actionCoroutine = Action;
        StartCoroutine(_actionCoroutine);
    }

    public virtual void StopAction()
    {
        IsActionGoing = false;
        StopCoroutine(_actionCoroutine);
    }

    protected abstract IEnumerator Coroutine();
}