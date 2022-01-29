using System;
using UnityEngine;

public abstract class EffectsController : CoroutineInsteadUpdateUser
{
    [SerializeField] protected float _maxEffectTime;

    public Action<float> CurveTimeChanged { get; set; }

    public override float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            CurveTimeChanged?.Invoke(_curveTime);
        }
    }

    public override void InvokeCoroutine()
    {
        CurveTargetTime = GetEffectTargetTime();
        base.InvokeCoroutine();
    }

    private new void Start()
    {
        base.Start();
        SubscribeToActions();
    }

    protected void OnDestroy()
    {
        UnsubscribeFromActions();
    }

    protected abstract float GetEffectTargetTime();
    protected abstract void SubscribeToActions();
    protected abstract void UnsubscribeFromActions();
}