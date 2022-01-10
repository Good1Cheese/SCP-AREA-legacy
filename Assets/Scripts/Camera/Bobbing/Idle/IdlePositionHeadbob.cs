using UnityEngine;

public class IdlePositionHeadbob : IdleCameraHeadbob
{
    protected Vector3 _newPosition = Vector3.zero;

    protected new void Awake()
    {
        base.Awake();
        _transform = transform;
    }

    protected void Update()
    {
        ActivateHeadbob();
    }

    protected virtual void ActivateHeadbob()
    {
        _curveTime += Time.deltaTime;

        _newPosition.x = GetCurveValue(_xAxis);
        _newPosition.y = GetCurveValue(_yAxis);
        _newPosition.z = GetCurveValue(_zAxis);
        _transform.localPosition = _newPosition;
    }
}