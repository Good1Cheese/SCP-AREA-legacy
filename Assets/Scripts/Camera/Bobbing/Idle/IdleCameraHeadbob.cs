using System.Collections;
using UnityEngine;

public class IdleCameraHeadbob : CameraHeadbob
{
    [SerializeField] protected IdleHeadbobCurve _xAxis;
    [SerializeField] protected IdleHeadbobCurve _yAxis;
    [SerializeField] protected IdleHeadbobCurve _zAxis;

    WaitForSeconds _timeoutBeforeRandomize;
    private float _curveValueMultipliyer = 1;

    public float CurveMultipliyer { set => _curveValueMultipliyer = value; }
    private float DelayBeforeRandomize { get => _xAxis.curve.GetLastKeyFrame().time / 2; }

    protected void Awake()
    {
        _timeoutBeforeRandomize = new WaitForSeconds(DelayBeforeRandomize);
        StartCoroutine(RandomizeCoroutine());
    }

    protected void Start()
    {
        _xAxis.CameraHeadbob = this;
        _yAxis.CameraHeadbob = this;
        _zAxis.CameraHeadbob = this;

        CurveChanged?.Invoke();
    }

    private IEnumerator RandomizeCoroutine()
    {
        yield return _timeoutBeforeRandomize;

        Randomized?.Invoke();
    }

    public override float GetCurveValue(HeadbobCurve HeadBobCurve)
    {
        return base.GetCurveValue(HeadBobCurve) * _curveValueMultipliyer;
    }
}