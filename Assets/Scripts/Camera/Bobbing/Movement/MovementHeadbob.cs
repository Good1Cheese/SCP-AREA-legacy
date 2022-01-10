using System;
using System.Collections;
using UnityEngine;
using Zenject;

public abstract class MovementHeadbob : CameraHeadbob
{
    [SerializeField] protected MovementHeadbobCurve _leftStepXAxis;
    [SerializeField] protected MovementHeadbobCurve _rightStepXAxis;
    [SerializeField] protected MovementHeadbobCurve _yAxis;

    [Inject(Id = "Camera")] private readonly Transform _mainCamera;

    IEnumerator _enumerator;
    private float _targetTime;
    private MovementHeadbobCurve _currentStepXAxis;
    protected Vector3 _newPosition = Vector3.zero;

    protected abstract MoveController MoveController { get; }

    private void Start()
    {
        MoveController.OnLeftStep += ActivateLeftStepHeadbob;
        MoveController.OnRightStep += ActivateRightStepHeadbob;
        MoveController.UseStopped += StopActivating;
    }

    private void Awake()
    {
        _transform = _mainCamera.transform;
        _targetTime = _leftStepXAxis.curve.GetLastKeyFrame().time;
    }

    private void ActivateLeftStepHeadbob()
    {
        _currentStepXAxis = _leftStepXAxis;
        ActivateHeadbob();
    }

    private void ActivateRightStepHeadbob()
    {
        _currentStepXAxis = _rightStepXAxis;
        ActivateHeadbob();
    }

    private void StopActivating()
    {
        StopCoroutine(_enumerator);
    }

    protected void ActivateHeadbob()
    {
        float x = _transform.localPosition.x;
        float y = _transform.localPosition.y;

        _currentStepXAxis.SetFirstPointValue(in x);
        _yAxis.SetFirstPointValue(in y);

        _enumerator = ActivateHeadbobCoroutine();
        StartCoroutine(_enumerator);
    }

    private IEnumerator ActivateHeadbobCoroutine()
    {
        while (_curveTime < _targetTime)
        {
            _curveTime += Time.deltaTime;

            _newPosition.x = GetCurveValue(_currentStepXAxis);
            _newPosition.y = GetCurveValue(_yAxis);
            _transform.localPosition = _newPosition;

            yield return null;
        }

        _curveTime = 0;
    }

    private void OnDestroy()
    {
        MoveController.OnLeftStep -= ActivateLeftStepHeadbob;
        MoveController.OnRightStep -= ActivateRightStepHeadbob;
    }
}