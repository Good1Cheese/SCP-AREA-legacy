using System;
using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fov;

    [Inject] private readonly Camera _mainCamera;

    private float _targetTime = -1;
    private Func<bool> _condition;
    private sbyte _deltaTimeMultipliyer;
    private bool _fovCalculated;

    public float FovTime { get; set; }

    public void SetFov(float targerFovTime)
    {
        CalculateFov(targerFovTime);

        if (_fovCalculated) { return; }

        if (_condition.Invoke())
        {
            _fovCalculated = true;
            return;
        }

        FovTime += Time.deltaTime * _deltaTimeMultipliyer;
        _mainCamera.fieldOfView = _fov.Evaluate(FovTime);
    }

    private void CalculateFov(float targerFovTime)
    {
        if (_targetTime == targerFovTime) { return; }

        _fovCalculated = false;
        _targetTime = targerFovTime;

        if (FovTime > targerFovTime)
        {
            _condition = () => FovTime <= targerFovTime;
            _deltaTimeMultipliyer = -1;

            return;
        }

        _condition = () => FovTime >= targerFovTime;
        _deltaTimeMultipliyer = 1;
    }
}