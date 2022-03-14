using UnityEngine;

public class SideHeadBob : CurveInputUser
{
    [SerializeField] private AnimationCurve _curve;

    private CurrentRotate _currentRotate;

    private void Awake()
    {
        _topCurveTimeLimit = _curve.GetLastKeyFrame().time;
        _bottomCurveTimeLimit = -_topCurveTimeLimit;

        _currentRotate = new CurrentRotate(() => 0,
                                           () => 0,
                                           () => _curve.Evaluate(_curveTime),
                                           transform); 
    }

    private void LateUpdate()
    {
        CalculateCurveTime();
        _currentRotate.Rotate();
    }
}