using UnityEngine;
using Zenject;

public class VerticalLean : DirectionLean
{
    [Inject] readonly private SlowWalk _slowWalk;

    private Vector3 _zero = Vector3.zero;
    private Vector3 _leanPosition = Vector3.zero;

    private new void Start()
    {
        base.Start();
        _slowWalk.Actions.NotUsing += Restore;
    }

    public override void Lean()
    {
        if (!_slowWalk.Using) { return; }

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
        _slowWalk.Actions.NotUsing -= Restore;
    }
}