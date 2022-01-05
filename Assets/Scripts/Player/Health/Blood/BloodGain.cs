using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class BloodGain : CoroutineWithDelayUser
{
    [SerializeField] private float _bloodGainTime;
    [SerializeField] private KeyCode _key;

    [Inject] readonly private PlayerBlood _playerBlood;

    private bool _isCoolDownDone = true;

    public Action Gained { get; set; }
    public Action GainStarted { get; set; }
    public float GainPerSecond { get; set; }

    private void Awake()
    {
        _playerBlood.BleedingStarted += StopCoroutine;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key)) { return; }

        StartWithoutInterrupt();
    }

    public override void StartWithoutInterrupt()
    {
        if (_playerBlood.IsCoroutineGoing || !_isCoolDownDone) { return; }

        base.StartWithoutInterrupt();
        StartCoroutine(StartCoolDown());
        GainPerSecond = (_playerBlood.MaxCurveTime - _playerBlood.CurveTime) / _bloodGainTime;
    }

    protected override IEnumerator Coroutine()
    {
        while (_playerBlood.CurveTime < _playerBlood.MaxCurveTime)
        {
            _playerBlood.CurveTime += Time.deltaTime * GainPerSecond;
            Gained?.Invoke();

            yield return null;
        }

        IsCoroutineGoing = false;
    }

    private IEnumerator StartCoolDown()
    {
        _isCoolDownDone = false;

        yield return _timeoutBeforeCoroutine;

        _isCoolDownDone = true;
    }

    private void OnDestroy()
    {
        _playerBlood.BleedingStarted -= StopCoroutine;
    }
}