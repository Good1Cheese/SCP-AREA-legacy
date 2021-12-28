using UnityEngine;

public class HorizontalLean : DirectionLean
{
    [SerializeField] private AnimationCurve _xLeanChange;

    private Quaternion _identity = Quaternion.identity;
    private Quaternion _leanRotation;
    private Vector3 _leanPosition;

    public override void Lean()
    {
        GetCurveTime();

        _leanPosition.y = transform.localPosition.y;
        _leanPosition.x = _xLeanChange.Evaluate(_curveTime);
        _leanRotation = Quaternion.Euler(0, 0, _curve.Evaluate(_curveTime));

        transform.localPosition = _leanPosition;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _leanRotation, _leanSmoothing * Time.deltaTime);
    }

    public override void Restore()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _identity, _leanSmoothing * Time.deltaTime);
    }
}