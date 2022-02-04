using UnityEngine;

public abstract class CameraHeadBob : MonoBehaviour
{
    protected abstract float �urveTime { get; }

    public virtual float GetCurveValue(HeadboBCurve HeadBobCurve)
    {
        return HeadBobCurve.curve.Evaluate(�urveTime);
    }
}