using UnityEngine;
using Zenject;

public class VerticalPeek : DirectionPeek
{
    [Inject] readonly private SlowWalkController _slowWalkController;

    private Vector3 _zero = Vector3.zero;
    private Vector3 _peekPosition = Vector3.zero;

    public override void Peek()
    {
        if (!_slowWalkController.IsMoving) { return; }

        GetPeekTime();

        _peekPosition.y = _peekCurve.Evaluate(_peekTime);
        transform.localPosition = _peekPosition;
    }

    public override void Restore()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _zero, _peekSmoothing * Time.deltaTime);
    }
}