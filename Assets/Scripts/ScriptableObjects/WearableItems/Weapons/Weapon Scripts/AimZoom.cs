using UnityEngine;
using Zenject;

public class AimZoom : MonoBehaviour
{
    [SerializeField] private RiseableCurve _fovCurve;

    private Camera _playerCamera;
    private WeaponAim _weaponAim;

    [Inject]
    private void Inject(Camera playerCamera, WeaponAim weaponAim)
    {
        _playerCamera = playerCamera;
        _weaponAim = weaponAim;
    }

    private void Awake()
    {
        _fovCurve.InitializeDefault();
    }

    private void Start()
    {
        _weaponAim.Aimed += _fovCurve.Rise;
        _weaponAim.Unaimed += _fovCurve.Decrease;
        _fovCurve.Changed += UpdateFOV;
    }

    private void UpdateFOV()
    {
        _playerCamera.fieldOfView = _fovCurve.GetCurrent();
    }

    private void OnDestroy()
    {
        _weaponAim.Aimed -= _fovCurve.Rise;
        _weaponAim.Unaimed -= _fovCurve.Decrease;
        _fovCurve.Changed -= UpdateFOV;
    }
}