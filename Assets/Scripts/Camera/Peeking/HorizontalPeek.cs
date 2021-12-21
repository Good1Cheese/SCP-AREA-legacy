using UnityEngine;

public class HorizontalPeek : DirectionPeek
{
    [SerializeField] private AnimationCurve _xPeekChange;

    private Quaternion _identity = Quaternion.identity;
    private Quaternion _peekRotation;
    private Vector3 _peekPosition;

    public override void Peek()
    {
        GetPeekTime();

        _peekPosition.y = transform.localPosition.y;
        _peekPosition.x = _xPeekChange.Evaluate(_peekTime);
        _peekRotation = Quaternion.Euler(0, 0, _peekCurve.Evaluate(_peekTime));

        transform.localPosition = _peekPosition;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _peekRotation, _peekSmoothing * Time.deltaTime);
    }

    public override void Restore()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _identity, _peekSmoothing * Time.deltaTime);
    }
}