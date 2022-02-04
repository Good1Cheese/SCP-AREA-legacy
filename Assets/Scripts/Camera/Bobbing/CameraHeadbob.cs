using UnityEngine;

public abstract class CameraHeadBob : MonoBehaviour
{
    protected abstract float ÑurveTime { get; }

    public virtual float GetCurveValue(HeadboBCurve HeadBobCurve)
    {
        return HeadBobCurve.curve.Evaluate(ÑurveTime);
    }
}