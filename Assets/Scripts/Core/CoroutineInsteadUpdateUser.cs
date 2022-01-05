using System;
using System.Collections;
using UnityEngine;

public abstract class CoroutineInsteadUpdateUser : CoroutineUser
{
    protected sbyte _deltaTimeMultipliyer;
    protected Func<bool> _condition;
    protected float _curveTime;

    public float CurveTargetTime { get; set; }
    public abstract float CurveTime { get; set; }

    public virtual void UpdateCoroutine()
    {
        GetConditionAndDeltaTimeMuitipliyer();
        StopCoroutine();
        StartCoroutine(Coroutine());
    }

    protected override IEnumerator Coroutine()
    {
        while (_condition())
        {
            CurveTime += Time.deltaTime * _deltaTimeMultipliyer;

            yield return null;
        }

        IsCoroutineGoing = false;
    }

    protected virtual void GetConditionAndDeltaTimeMuitipliyer()
    {
        if (CurveTargetTime < CurveTime)
        {
            DecreaseCurveTime();
            return;
        }

        IncreaseCurveTime();
    }

    protected void IncreaseCurveTime()
    {
        _condition = () => CurveTime < CurveTargetTime;
        _deltaTimeMultipliyer = 1;
    }

    protected void DecreaseCurveTime()
    {
        _condition = () => CurveTime > CurveTargetTime;
        _deltaTimeMultipliyer = -1;
    }
}