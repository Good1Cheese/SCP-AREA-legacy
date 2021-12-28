using System;
using UnityEngine;

public abstract class EffectsController : MonoBehaviour
{
    [SerializeField] protected float _maxEffectTime;

    private sbyte _curveTimeMultiplayer;
    private Func<bool> _timeChangeCondition;

    public Action<float> EffectsTimeChanged { get; set; }
    public float CurveTargetTime { get; set; }
    public float CurveCurrentTime { get; set; }

    private void Start()
    {
        SetEffectTimeAfterDamage();

        SubscribeToActions();
    }

    private void Update()
    {
        if (_timeChangeCondition.Invoke())
        {
            CurveCurrentTime += Time.deltaTime * _curveTimeMultiplayer;
            EffectsTimeChanged?.Invoke(CurveCurrentTime);
        }
    }

    public void SetEffectTimeAfterDamage()
    {
        SetCurveTimeData(() => CurveCurrentTime < CurveTargetTime, 1);
    }

    public void SetEffectTimeAfterHeal()
    {
        SetCurveTimeData(() => CurveCurrentTime > CurveTargetTime, -1);
    }

    private void SetCurveTimeData(Func<bool> func, sbyte value)
    {
        CurveTargetTime = GetEffectTargetTime();
        _timeChangeCondition = func;
        _curveTimeMultiplayer = value;
    }


    protected void OnDestroy()
    {
        UnsubscribeFromActions();
    }

    protected abstract float GetEffectTargetTime();
    protected abstract void SubscribeToActions();
    protected abstract void UnsubscribeFromActions();
}