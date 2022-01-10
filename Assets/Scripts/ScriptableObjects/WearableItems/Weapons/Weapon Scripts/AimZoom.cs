using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class AimZoom : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;

    [Inject] private readonly Camera _mainCamera;
    [Inject] private readonly WeaponAim _weaponAim;

    private IEnumerator _enumerator;
    private float _curveTime;
    private float _maxCurveTime;

    private void Start()
    {
        _maxCurveTime = _curve.GetLastKeyFrame().time;
        _enumerator = ZoomCoroutine(() => _curveTime > 0, -1);

        _weaponAim.Aimed += Zoom;
        _weaponAim.Unaimed += Unzoom;
    }

    private void Zoom() => Zoom(() => _curveTime < _maxCurveTime, 1);
    private void Unzoom() => Zoom(() => _curveTime > 0, -1);

    private void Zoom(Func<bool> condition, sbyte deltaTimeMultipliyer)
    {
        StopCoroutine(_enumerator);
        _enumerator = ZoomCoroutine(condition, deltaTimeMultipliyer);
        StartCoroutine(_enumerator);
    }

    private IEnumerator ZoomCoroutine(Func<bool> condition, sbyte deltaTimeMultipliyer)
    {
        while (condition())
        {
            _curveTime += Time.deltaTime * deltaTimeMultipliyer;
            _mainCamera.fieldOfView = _curve.Evaluate(_curveTime);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        _weaponAim.Aimed -= Zoom;
        _weaponAim.Unaimed -= Unzoom;
    }
}