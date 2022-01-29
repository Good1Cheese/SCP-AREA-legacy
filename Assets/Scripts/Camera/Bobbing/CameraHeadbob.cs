using UnityEngine;

public abstract class CameraHeadBob : MonoBehaviour
{
    protected float _curveTime;

    public virtual float GetCurveValue(HeadboBCurve HeadBobCurve)
    {
        return HeadBobCurve.curve.Evaluate(_curveTime);
    }
}