using UnityEngine;
using Zenject;

public class RotationHeadBobWhileRun : MovementHeadBob
{
    [SerializeField] private MovementHeadBobCurve _stepZAxis;

    private float _x;
    private float _y;
    private float _z;

    private CurrentRotate _currentRotate;

    [Inject]
    private void Construct(Run runController)
    {
        _move = runController;
    }

    private void Awake()
    {
        _currentRotate = new CurrentRotate(() => GetCurveValue(_stepXAxis) * _curveValueMultipliyer,
                                           () => GetCurveValue(_yAxis) * _curveValueMultipliyer,
                                           () => GetCurveValue(_stepZAxis) * _curveValueMultipliyer,
                                           transform);
    }

    protected override void ContinueFromLastKey()
    {
        float x = _x * _curveValueMultipliyer;
        float y = _y * _curveValueMultipliyer;
        float z = _z * _curveValueMultipliyer;

        _stepXAxis.SetFirstPointValue(in x);
        _yAxis.SetFirstPointValue(in y);
        _stepZAxis.SetFirstPointValue(in z);
    }

    protected override void OnCurveTimeChanged() => _currentRotate.Rotate();
}