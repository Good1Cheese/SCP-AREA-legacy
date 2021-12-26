using UnityEngine;
using Zenject;

public class VerticalLean : DirectionLean
{
    [Inject] readonly private SlowWalkController _slowWalkController;

    private Vector3 _zero = Vector3.zero;
    private Vector3 _leanPosition = Vector3.zero;

    public override void Lean()
    {
        if (!_slowWalkController.IsMoving) { return; }

        GetLeanTime();

        _leanPosition.y = _peekCurve.Evaluate(_leanTime);
        transform.localPosition = _leanPosition;
    }

    public override void Restore()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _zero, _leanSmoothing * Time.deltaTime);
    }
}