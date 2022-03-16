using System;
using UnityEngine;

[Serializable]
public class RiseableCurve : ICurveValueGetter
{
    [SerializeField] protected AnimationCurve _curve;

    protected float _curveTime;
    private CurveCommand _riseCommand;
    private CurveCommand _decreaseCommand;

    public AnimationCurve Curve => _curve;
    public Action Changed { get; set; }

    public float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            Changed?.Invoke();
        }
    }

    public void Initialize(Func<float, bool> riseCommandCondition, Func<float, bool> decreaseCommandCondition)
    {
        InitializeDefault();
        _riseCommand.Condition = riseCommandCondition;
        _decreaseCommand.Condition = decreaseCommandCondition;
    }

    public virtual void InitializeDefault()
    {
        var riseCommand = new CurveCommand(this, curveValue => curveValue > Curve.GetLastKeyFrame().value, true);
        var decreaseCommand = new CurveCommand(this, curveValue => curveValue < Curve.GetFirstKeyFrame().value, false);

        _riseCommand = riseCommand;
        _decreaseCommand = decreaseCommand;
    }

    public float GetCurrent() => _curve.Evaluate(_curveTime);

    public void Rise() => _riseCommand.CallCommand();
    public void Decrease() => _decreaseCommand.CallCommand();
}