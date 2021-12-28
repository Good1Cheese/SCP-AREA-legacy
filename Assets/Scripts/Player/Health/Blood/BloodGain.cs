using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class BloodGain : CoroutineUser
{
    [SerializeField] private float _bloodGainTime;
    [SerializeField] private KeyCode _key;

    [Inject] readonly private PlayerBlood _playerBlood;

    public Action Gained { get; set; }
    public Action GainStarted { get; set; }

    private void Update()
    {
        if (!Input.GetKeyDown(_key)) { return; }

        StartAction();
    }

    protected override void StartAction()
    {
        base.StartAction();
        GainStarted?.Invoke();
    }

    protected override IEnumerator Coroutine()
    {
        float gainPerSecond = (_playerBlood.MaxCurveTime - _playerBlood.CurveTime) / _bloodGainTime;

        while (_playerBlood.CurveTime < _playerBlood.MaxCurveTime)
        {
            _playerBlood.CurveTime += Time.deltaTime * gainPerSecond;
            Gained?.Invoke();

            yield return null;
        }

        IsActionGoing = false;
    }
}