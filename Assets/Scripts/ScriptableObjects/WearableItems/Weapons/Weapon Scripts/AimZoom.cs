using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class AimZoom : CoroutineInsteadUpdateUser
{
    [SerializeField] private AnimationCurve _curve;

    private Camera _playerCamera;
    private WeaponAim _weaponAim;

    private float _maxCurveTime;

    public override float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            _playerCamera.fieldOfView = _curve.Evaluate(_curveTime);
        }
    }

    [Inject]
    private void Inject(Camera playerCamera, WeaponAim weaponAim)
    {
        _playerCamera = playerCamera;
        _weaponAim = weaponAim;
    }

    private new void Start()
    {
        base.Start();
        _maxCurveTime = _curve.GetLastKeyFrame().time;

        _weaponAim.Aimed += Zoom;
        _weaponAim.Unaimed += Unzoom;
    }

    private void Zoom()
    {
        CurveTargetTime = _maxCurveTime;
        InvokeCoroutine();
    }

    private void Unzoom()
    {
        CurveTargetTime = 0;
        InvokeCoroutine();
    }

    private void OnDestroy()
    {
        _weaponAim.Aimed -= Zoom;
        _weaponAim.Unaimed -= Unzoom;
    }
}