using UnityEngine;

public abstract class DirectionPeek : MonoBehaviour
{
    [SerializeField] private float _peekTimeSmoothing;
    [SerializeField] private float _peekTimeLimit;

    [SerializeField] private KeyCode _positivKey;
    [SerializeField] private KeyCode _negativKey;

    [SerializeField] protected float _peekSmoothing;
    [SerializeField] protected AnimationCurve _peekCurve;

    protected float _peekTime;
        
    public float PeekTime { set => _peekTime = value; }

    public abstract void Peek();
    public abstract void Restore();

    protected void GetPeekTime()
    {
        bool isPositivKeyPressed = Input.GetKey(_positivKey);
        if (isPositivKeyPressed)
        {
            _peekTime += Time.deltaTime;
        }

        bool isNegativKeyPressed = Input.GetKey(_negativKey);
        if (isNegativKeyPressed)
        {
            _peekTime -= Time.deltaTime;
        }

        if (!isPositivKeyPressed && !isNegativKeyPressed)
        {
            _peekTime = Mathf.Lerp(_peekTime, 0, _peekTimeSmoothing * Time.deltaTime);
        }

        _peekTime = Mathf.Clamp(_peekTime, -_peekTimeLimit, _peekTimeLimit);
    }
}