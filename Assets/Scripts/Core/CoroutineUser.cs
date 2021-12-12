using System.Collections;
using UnityEngine;

public abstract class CoroutineUser : MonoBehaviour
{
    [SerializeField] private float _coroutineDelay;

    protected WaitForSeconds _coroutineTimeout;
    private IEnumerator _actionCoroutine;
    protected virtual IEnumerator Action => Coroutine();

    public bool IsActionGoing { get; set; }

    protected void Start()
    {
        _actionCoroutine = Action;
        _coroutineTimeout = new WaitForSeconds(_coroutineDelay);
    }

    public void StartAction()
    {
        if (IsActionGoing) { return; }

        IsActionGoing = true;
        _actionCoroutine = Action;
        StartCoroutine(_actionCoroutine);
    }

    public void StopAction()
    {
        IsActionGoing = false;
        StopCoroutine(_actionCoroutine);
    }

    protected abstract IEnumerator Coroutine();
}