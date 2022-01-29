using System.Collections;
using UnityEngine;

public abstract class MovementHeadBob : CameraHeadBob
{
    [SerializeField] protected Transform _transform;
    [SerializeField] protected MovementHeadBobCurve _stepXAxis;
    [SerializeField] protected MovementHeadBobCurve _yAxis;

    protected float _targetTime;
    protected sbyte _curveValueMultipliyer;
    protected Vector3 _newPosition = Vector3.zero;
    protected MoveController _moveController;

    private void Start()
    {
        _moveController.OnLeftStep += ActivateLeftStepHeadbob;
        _moveController.OnRightStep += ActivateRightStepHeadbob;
    }

    private void Awake()
    {
        _targetTime = _stepXAxis.curve.GetLastKeyFrame().time;
    }

    private void ActivateLeftStepHeadbob()
    {
        _curveValueMultipliyer = 1;
        ActivateHeadbob();
    }

    private void ActivateRightStepHeadbob()
    {
        _curveValueMultipliyer = -1;
        ActivateHeadbob();
    }

    protected void ActivateHeadbob()
    {
        ContinueFromLastKey();
        StartCoroutine(ActivateHeadbobCoroutine());
    }

    protected virtual void ContinueFromLastKey()
    {
        float x = _transform.localPosition.x * _curveValueMultipliyer;
        float y = _transform.localPosition.y;

        _stepXAxis.SetFirstPointValue(in x);
        _yAxis.SetFirstPointValue(in y);
    }

    private IEnumerator ActivateHeadbobCoroutine()
    {
        while (_curveTime < _targetTime)
        {
            _curveTime += Time.deltaTime;
            OnCurveTimeChanged();

            yield return null;
        }
        _curveTime = 0;
    }

    protected virtual void OnCurveTimeChanged()
    {
        _newPosition.x = GetCurveValue(_stepXAxis) * _curveValueMultipliyer;
        _newPosition.y = GetCurveValue(_yAxis);
        _transform.localPosition = _newPosition;
    }

    private void OnDestroy()
    {
        _moveController.OnLeftStep -= ActivateLeftStepHeadbob;
        _moveController.OnRightStep -= ActivateRightStepHeadbob;
    }
}