using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerBlood : CoroutineUser
{
    [SerializeField] private float _maxAmount;
    [SerializeField] private float _amount;
    [SerializeField] private float _curveTime;
    [SerializeField] private float _maxCurveTime;
    [SerializeField] private AnimationCurve _curve;

    [Inject] readonly private PlayerHealth _playerHealth;

    private float _lossMultipliyer;

    public Action Bled { get; set; }
    public Action Healed { get; set; }
    public Action Changed { get; set; }

    public float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            _amount = _curve.Evaluate(_curveTime);
            Changed?.Invoke();
        }
    }

    public float MaxCurveTime => _maxCurveTime;
    public float Amount => _amount;
    public float MaxAmount => _maxAmount;

    public void StartBleeding(float startCurveTimeLoss, float lossMultipliyer)
    {
        CurveTime -= startCurveTimeLoss;
        _lossMultipliyer = lossMultipliyer;
        StartAction();
    }

    protected override IEnumerator Coroutine()
    {
        while (_curveTime > 0)
        {
            CurveTime -= Time.deltaTime * _lossMultipliyer;
            Bled?.Invoke();

            yield return null;
        }

        _playerHealth.Damage(_playerHealth.Amount);
        IsActionGoing = false;
    }
}