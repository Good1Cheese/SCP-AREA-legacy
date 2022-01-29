using System;
using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fovCurve;

    private Camera _mainCamera;
    private float _currentTargetFovTime = -1;
    private Func<bool> _condition;
    private sbyte _deltaTimeMultipliyer;
    private bool _fovCalculated;

    public float CurveTime { get; set; }

    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    public void SetFov(float targerFovTime)
    {
        CalculateFov(targerFovTime);

        if (_fovCalculated) { return; }

        if (_condition.Invoke())
        {
            _fovCalculated = true;
            return;
        }

        CurveTime += Time.deltaTime * _deltaTimeMultipliyer;
        _mainCamera.fieldOfView = _fovCurve.Evaluate(CurveTime);
    }

    private void CalculateFov(float targerFovTime)
    {
        if (_currentTargetFovTime == targerFovTime) { return; }

        _fovCalculated = false;
        _currentTargetFovTime = targerFovTime;

        if (CurveTime > targerFovTime)
        {
            _condition = () => CurveTime < targerFovTime;
            _deltaTimeMultipliyer = -1;

            return;
        }

        _condition = () => CurveTime > targerFovTime;
        _deltaTimeMultipliyer = 1;
    }
}