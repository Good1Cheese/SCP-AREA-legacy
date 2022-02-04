using System;
using System.Threading.Tasks;
using UnityEngine;

public class CurveCommand
{
    private readonly RiseableCurve _riseableCurve;
    private readonly int _deltaTimeMultiplier;

    public CurveCommand(RiseableCurve riseableCurve, Func<float, bool> condition, bool isRiseable)
    {
        _riseableCurve = riseableCurve;
        Condition = condition;
        _deltaTimeMultiplier = (isRiseable) ? 1 : -1;
    }

    public Func<float, bool> Condition { get; set; }

    public void CallCommand() => DoCommand();

    public async void DoCommand()
    {
        while (Condition.Invoke(_riseableCurve.Evaluate()))
        {
            _riseableCurve.CurveTime += Time.deltaTime * _deltaTimeMultiplier;

            await Task.Yield();
        }
    }
}