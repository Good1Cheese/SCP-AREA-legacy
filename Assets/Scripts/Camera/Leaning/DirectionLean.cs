using System.Collections;
using UnityEngine;
using Zenject;

public abstract class DirectionLean : CurveInputUser
{
    [SerializeField] private float _curveTimeLimit;
    [SerializeField] private float _curveChangeTime;

    [SerializeField] protected float _leanSmoothing;
    [SerializeField] protected AnimationCurve _curve;

    [Inject] readonly protected GameObjectTrigger _cameraTrigger;

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

    public abstract void Lean();
    public abstract void Restore();

    protected void OnDestroy()
    {
        _cameraTrigger.TriggerEnter -= ReduceCurveTimeStrenght;
        _cameraTrigger.TriggerStay -= SetCurveLimit;
        _cameraTrigger.TriggerExit -= SetDefaultCurveLimit;
    }
}