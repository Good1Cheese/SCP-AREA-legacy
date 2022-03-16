using System;
using System.Threading.Tasks;
using UnityEngine;

public class CurveCommand
{
    private readonly ICurveValueGetter _curveValueGetter;
    private readonly int _deltaTimeMultiplier;

    public CurveCommand(ICurveValueGetter curveValueGetter, Func<float, bool> condition, bool isRiseable)
    {
        _curveValueGetter = curveValueGetter;
        Condition = condition;
        _deltaTimeMultiplier = isRiseable ? 1 : -1;
    }

    public Func<float, bool> Condition { get; set; }

    public void CallCommand() => DoCommand();

    public async void DoCommand()
    {
        while (Condition.Invoke(_curveValueGetter.GetCurrent()))
        {
            _curveValueGetter.CurveTime += Time.deltaTime * _deltaTimeMultiplier;

            await Task.Yield();
        }
    }
}

public interface ICurveValueGetter
{
    float GetCurrent();
    public float CurveTime { get; set; }
}