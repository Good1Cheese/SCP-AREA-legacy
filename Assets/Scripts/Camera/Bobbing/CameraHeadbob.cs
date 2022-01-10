using System;
using UnityEngine;

public abstract class CameraHeadbob : MonoBehaviour
{
    protected Transform _transform;
    protected float _curveTime;

    public Action Randomized { get; set; }
    public Action CurveChanged { get; set; }

    public virtual float GetCurveValue(HeadbobCurve HeadBobCurve)
    {
        return HeadBobCurve.curve.Evaluate(_curveTime);
    }
}