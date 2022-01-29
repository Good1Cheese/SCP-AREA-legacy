using UnityEngine;

public abstract class CurveInputUser : MonoBehaviour
{
    [SerializeField] private KeyCode _positivKey;
    [SerializeField] private KeyCode _negativKey;
    [SerializeField] private float _curveTimeReturnSmoothing;

    protected float _curveTime;
    protected float _topCurveTimeLimit;
    protected float _bottomCurveTimeLimit;

    protected void CalculateCurveTime()
    {
        GetCurveTime();
        _curveTime = Mathf.Clamp(_curveTime, _bottomCurveTimeLimit, _topCurveTimeLimit);
    }

    protected void GetCurveTime()
    {
        if (Input.GetKey(_positivKey))
        {
            _curveTime += Time.deltaTime;
            return;
        }

        if (Input.GetKey(_negativKey))
        {
            _curveTime -= Time.deltaTime;
            return;
        }

        _curveTime = Mathf.Lerp(_curveTime, 0, _curveTimeReturnSmoothing * Time.deltaTime);
    }
}