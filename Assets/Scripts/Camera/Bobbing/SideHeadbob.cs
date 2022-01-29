using UnityEngine;

public class SideHeadBob : CurveInputUser
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Transform _transform;

    private Quaternion _newRotation = Quaternion.identity;

    private void Awake()
    {
        _topCurveTimeLimit = _curve.GetLastKeyFrame().time;
        _bottomCurveTimeLimit = -_topCurveTimeLimit;
    }

    private void Update()
    {
        CalculateCurveTime();

        _newRotation = Quaternion.Euler(0, 0, _curve.Evaluate(_curveTime));
        _transform.localRotation = _newRotation;
    }
}