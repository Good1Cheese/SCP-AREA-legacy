using System.Collections;
using UnityEngine;

public abstract class MovementHeadBob : CameraHeadBob
{
    [SerializeField] protected Transform _transform;
    [SerializeField] protected MovementHeadBobCurve _stepXAxis;
    [SerializeField] protected MovementHeadBobCurve _yAxis;

    private static float _curveTime;
    private float _targetTime;
    private Vector3 _newPosition = Vector3.zero;
    protected sbyte _curveValueMultipliyer;
    protected MoveController _moveController;

    protected override float СurveTime => _curveTime;

    private void Awake()
    {
        _targetTime = _stepXAxis.curve.GetLastKeyFrame().time;
    }

    private void Start()
    {
        _moveController.OnLeftStep += ActivateLeftStepHeadbob;
        _moveController.OnRightStep += ActivateRightStepHeadbob;
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
        print("Started");
        while (_curveTime < _targetTime)
        {
            _curveTime += Time.fixedDeltaTime;
            OnCurveTimeChanged();

            yield return null;
        }

        _curveTime = 0;
        print("Ended");
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