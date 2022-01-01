using System.Collections;
using UnityEngine;

public abstract class CoroutineUserSaving : DataSaving
{
    protected abstract CoroutineWithDelayUser CoroutineWithDelayUser { get; }

    public float currentDelay;
    private IEnumerator _coroutine;

    private void Start()
    {
        CoroutineWithDelayUser.CoroutineStarted += StartCountdown;
        _coroutine = StartCountdownCoroutine();
    }

    private void StartCountdown()
    {
        StopCoroutine(_coroutine);
        StartCoroutine();
    }

    private void StartCoroutine()
    {
        _coroutine = StartCountdownCoroutine();
        StartCoroutine(_coroutine);
    }

    private IEnumerator StartCountdownCoroutine()
    {
        currentDelay = CoroutineWithDelayUser.DelayBeforeCoroutine;

        while (currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
            yield return null;
        }

        if (currentDelay < 0) 
        {
            currentDelay = 0; 
        }
    }

    private void OnDestroy()
    {
        CoroutineWithDelayUser.CoroutineStarted -= StartCountdown;
    }
}