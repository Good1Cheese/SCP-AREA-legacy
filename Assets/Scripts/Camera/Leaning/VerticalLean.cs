using UnityEngine;
using Zenject;

public class VerticalLean : DirectionLean
{
    [Inject] readonly private SlowWalkController _slowWalkController;

    private Vector3 _zero = Vector3.zero;
    private Vector3 _leanPosition = Vector3.zero;

    private new void Start()
    {
        base.Start();
        _slowWalkController.NotUsing += Restore;
    }

    public override void Lean()
    {
        if (!_slowWalkController.IsMoving) { return; }

        GetCurveTime();

        _leanPosition.y = _curve.Evaluate(_curveTime);
        transform.localPosition = _leanPosition;
    }

    public override void Restore()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _zero, _leanSmoothing * Time.deltaTime);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _slowWalkController.NotUsing -= Restore;
    }
}