using UnityEngine;

public class IdleRotationHeadbob : IdleHeadbob
{
    private Quaternion _newRotation = Quaternion.identity;

    private void Update()
    {
        _curveTime += Time.deltaTime;

        _newRotation = Quaternion.Euler(GetCurveValue(_xPosition),
                                        GetCurveValue(_yPosition),
                                        GetCurveValue(_zPosition));

        transform.localRotation = _newRotation;
    }
}