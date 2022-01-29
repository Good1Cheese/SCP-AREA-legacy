using UnityEngine;
using Zenject;

public class RotationHeadBobWhileRun : MovementHeadBob
{
    [SerializeField] private MovementHeadBobCurve _stepZAxis;

    private float _x;
    private float _y;
    private float _z;

    [Inject]
    private void Construct(RunController runController)
    {
        _moveController = runController;
    }

    protected override void ContinueFromLastKey()
    {
        float value = _x * _curveValueMultipliyer;
        float value1 = _y * _curveValueMultipliyer;
        float value2 = _z * _curveValueMultipliyer;

        _stepXAxis.SetFirstPointValue(in value);
        _yAxis.SetFirstPointValue(in value1);
        _stepZAxis.SetFirstPointValue(in value2);
    }

    protected override void OnCurveTimeChanged()
    {
        _x = GetCurveValue(_stepXAxis) * _curveValueMultipliyer;
        _y = GetCurveValue(_yAxis) * _curveValueMultipliyer;
        _z = GetCurveValue(_stepZAxis) * _curveValueMultipliyer;

        _transform.localRotation = Quaternion.Euler(_x, _y, _z);
    }
}