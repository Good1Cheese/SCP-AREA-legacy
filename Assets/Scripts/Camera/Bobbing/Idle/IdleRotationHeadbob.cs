public class IdleRotationHeadBob : IdleCameraHeadBob
{
    private CurrentRotate _currentRotate;

    private void Awake()
    {
        _currentRotate = new CurrentRotate(() => GetCurveValue(xAxis),
                                           () => GetCurveValue(yAxis),
                                           () => GetCurveValue(zAxis),
                                           transform);
    }

    protected override void ActivateHeadbob() => _currentRotate.Rotate();
}