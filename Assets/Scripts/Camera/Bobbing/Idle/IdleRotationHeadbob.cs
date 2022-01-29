using UnityEngine;

public class IdleRotationHeadBob : IdleCameraHeadBob
{
    private Quaternion _newRotation = Quaternion.identity;

    protected override void ActivateHeadbob()
    {
        _newRotation = Quaternion.Euler(GetCurveValue(xAxis),
                                        GetCurveValue(yAxis),
                                        GetCurveValue(zAxis));

        transform.localRotation = _newRotation;
    }
}