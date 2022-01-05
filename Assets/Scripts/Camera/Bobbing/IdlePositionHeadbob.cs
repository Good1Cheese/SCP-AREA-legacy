using UnityEngine;

public class IdlePositionHeadbob : IdleHeadbob
{
    private Vector3 _newPosition = Vector3.zero;

    private void Update()
    {
        _curveTime += Time.deltaTime;

        _newPosition.x = GetCurveValue(_xPosition);
        _newPosition.y = GetCurveValue(_yPosition);
        _newPosition.z = GetCurveValue(_zPosition);
        transform.localPosition = _newPosition;
    }
}