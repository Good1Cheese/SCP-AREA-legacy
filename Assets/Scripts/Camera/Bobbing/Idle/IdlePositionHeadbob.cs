using UnityEngine;

public class IdlePositionHeadBob : IdleCameraHeadBob
{
    private Transform _transform;
    protected Vector3 _newPosition = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
    }

    protected override void ActivateHeadbob()
    {
        _newPosition.x = GetCurveValue(xAxis);
        _newPosition.y = GetCurveValue(yAxis);
        _newPosition.z = GetCurveValue(zAxis);

        _transform.localPosition = _newPosition;
    }
}