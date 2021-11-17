using System;
using UnityEngine;
using Zenject;

public class InjuryEffectsController : MonoBehaviour
{
    protected const float MAX_EFFECT_CURVE_TIME = 1.6f;

    [Inject] protected readonly PlayerHealth _playerHealth;
    private sbyte _curveTimeMultiplayer;
    private Func<bool> _timeChangeCondition;

    public Action<float> OnEffectTimeChanging { get; set; }
    public float CurveTargetTime { get; set; }
    public float CurveCurrentTime { get; set; }

    private void Start()
    {
        SetCurveTimeDataAfterDamage();

        _playerHealth.OnPlayerGetsDamage += SetCurveTimeDataAfterDamage;
        _playerHealth.OnPlayerHeals += SetCurveTimeDataAfterBleedDamage;
        _playerHealth.OnPlayerDies += OnDestroy;
    }

    private void Update()
    {
        if (_timeChangeCondition.Invoke())
        {
            CurveCurrentTime += Time.deltaTime * _curveTimeMultiplayer;
            OnEffectTimeChanging?.Invoke(CurveCurrentTime);
        }
    }

    public void SetCurveTimeDataAfterDamage()
    {
        SetCurveTimeData(() => CurveCurrentTime < CurveTargetTime, 1);
    }

    public void SetCurveTimeDataAfterBleedDamage()
    {
        SetCurveTimeData(() => CurveCurrentTime > CurveTargetTime, -1);
    }

    private void SetCurveTimeData(Func<bool> func, sbyte value)
    {
        CurveTargetTime = GetEffectTargetTime();
        _timeChangeCondition = func;
        _curveTimeMultiplayer = value;
    }

    private float GetEffectTargetTime()
    {
        return MAX_EFFECT_CURVE_TIME * (_playerHealth.MaxAmount - _playerHealth.Amount) / 100;
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerGetsDamage -= SetCurveTimeDataAfterDamage;
        _playerHealth.OnPlayerHeals -= SetCurveTimeDataAfterBleedDamage;
        _playerHealth.OnPlayerDies -= OnDestroy;
    }
}