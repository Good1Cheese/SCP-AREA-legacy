using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerBlood : CoroutineUser
{
    [SerializeField] private AnimationCurve _curve;

    [Inject] readonly private PlayerHealth _playerHealth;

    private float _curveTime;
    private float _lossMultipliyer;

    public float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            Amount = _curve.Evaluate(_curveTime);
            Changed?.Invoke();
        }
    }

    public float MaxCurveTime { get; set; }
    public float MaxAmount { get; set; }
    public float Amount { get; set; }

    public Action Bled { get; set; }
    public Action BleedingStarted { get; set; }
    public Action Changed { get; set; }
    public float LossMultipliyer { get => _lossMultipliyer; }
    public float CurrentPercentage => (MaxAmount - Amount) / 100;

    private void Awake()
    {
        Keyframe keyframe = _curve.GetLastKeyFrame();

        _curveTime = keyframe.time;
        MaxCurveTime = _curveTime;
        Amount = keyframe.value;
        MaxAmount = Amount;
    }

    public void StartBleeding(float startCurveTimeLoss, float lossMultipliyer)
    {
        if (IsCoroutineGoing) { return; }

        CurveTime -= startCurveTimeLoss;
        _lossMultipliyer = lossMultipliyer;
        BleedingStarted?.Invoke();
        StartWithoutInterrupt();
    }

    protected override IEnumerator Coroutine()
    {
        while (_curveTime > 0)
        {
            CurveTime -= Time.deltaTime * LossMultipliyer;
            Bled?.Invoke();

            yield return null;
        }

        _playerHealth.Damage(_playerHealth.MaxAmount);
        IsCoroutineGoing = false;
    }
}