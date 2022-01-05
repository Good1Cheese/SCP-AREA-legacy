using System.Collections;
using UnityEngine;
using Zenject;

public abstract class DirectionLean : MonoBehaviour
{
    [SerializeField] private float _curveTimeSmoothing;
    [SerializeField] private float _curveTimeLimit;
    [SerializeField] private float _curveChangeTime;

    [SerializeField] private KeyCode _positivKey;
    [SerializeField] private KeyCode _negativKey;

    [SerializeField] protected float _leanSmoothing;
    [SerializeField] protected AnimationCurve _curve;

    [Inject] readonly protected GameObjectTrigger _cameraTrigger;

    protected float _curveTime;
    protected float _topCurveTimeLimit;
    protected float _bottomCurveTimeLimit;

    public float CurveTime { set => _curveTime = value; }

    protected void Start()
    {
        SetDefaultCurveLimit();

        _cameraTrigger.TriggerEnter += ReduceCurveTimeStrenght;
        _cameraTrigger.TriggerStay += SetCurveLimit;
        _cameraTrigger.TriggerExit += SetDefaultCurveLimit;
    }

    private void ReduceCurveTimeStrenght()
    {
        if (_curveTime == _curveTimeLimit || _curveTime == -_curveTimeLimit)
        {
            StartCoroutine(Cocoroutine(_curveTime / 2));
        }
    }

    private IEnumerator Cocoroutine(float targetLeanTime)
    {
        float elapsedTime = 0;

        while (elapsedTime < _curveChangeTime)
        {
            _curveTime = Mathf.Lerp(_curveTime, targetLeanTime, elapsedTime / _curveChangeTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private void SetCurveLimit()
    {
        if (_curveTime > 0)
        {
            _topCurveTimeLimit = _curveTime;
            return;
        }
        _bottomCurveTimeLimit = _curveTime;
    }

    protected void SetDefaultCurveLimit()
    {
        _topCurveTimeLimit = _curveTimeLimit;
        _bottomCurveTimeLimit = -_curveTimeLimit;
    }

    protected void GetCurveTime()
    {
        bool isPositivKeyPressed = Input.GetKey(_positivKey);
        bool isNegativKeyPressed = Input.GetKey(_negativKey);

        if (isPositivKeyPressed)
        {
            _curveTime += Time.deltaTime;
        }

        if (isNegativKeyPressed)
        {
            _curveTime -= Time.deltaTime;
        }

        if (!isPositivKeyPressed && !isNegativKeyPressed)
        {
            _curveTime = Mathf.Lerp(_curveTime, 0, _curveTimeSmoothing * Time.deltaTime);
        }

        _curveTime = Mathf.Clamp(_curveTime, _bottomCurveTimeLimit, _topCurveTimeLimit);
    }

    public abstract void Lean();
    public abstract void Restore();

    protected void OnDestroy()
    {
        _cameraTrigger.TriggerEnter -= ReduceCurveTimeStrenght;
        _cameraTrigger.TriggerStay -= SetCurveLimit;
        _cameraTrigger.TriggerExit -= SetDefaultCurveLimit;
    }
}