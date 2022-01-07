using UnityEngine;

public class IdleRotationHeadbob : CameraHeadbob
{
    private Quaternion _newRotation = Quaternion.identity;

    private void Update()
    {
        _curveTime += Time.deltaTime;

        _newRotation = Quaternion.Euler(GetCurveValue(_xAxis),
                                        GetCurveValue(_yAxis),
                                        GetCurveValue(_zAxis));
        transform.localRotation = _newRotation;
    }
}