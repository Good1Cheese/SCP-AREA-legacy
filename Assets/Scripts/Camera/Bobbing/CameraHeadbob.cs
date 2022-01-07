using System;
using System.Collections;
using UnityEngine;

public abstract class CameraHeadbob : MonoBehaviour
{
    [SerializeField] protected HeadbobCurve _xAxis;
    [SerializeField] protected HeadbobCurve _yAxis;
    [SerializeField] protected HeadbobCurve _zAxis;
    [SerializeField] protected float _delayBeforeRandomize;

    protected Transform _transform;
    protected float _curveTime;
    private float _curveValueMultipliyer = 1;
    WaitForSeconds _timeoutBeforeRandomize;

    public Action Randomized { get; set; }
    public Action CurveChanged { get; set; }
    public float CurveMultipliyer { set => _curveValueMultipliyer = value; }

    protected void Awake()
    {
        _timeoutBeforeRandomize = new WaitForSeconds(_delayBeforeRandomize);
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

    public float GetCurveValue(HeadbobCurve HeadBobCurve)
    {
        return HeadBobCurve.curve.Evaluate(_curveTime) * _curveValueMultipliyer;
    }
}