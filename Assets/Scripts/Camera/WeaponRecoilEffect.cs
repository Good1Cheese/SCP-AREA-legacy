using UnityEngine;
using Zenject;

public class WeaponRecoilEffect : MonoBehaviour
{
    [Inject] private readonly WeaponAim _weaponAim;
    [Inject] private readonly WeaponSlot _weaponSlot;

    private Weapon_SO _weapon_SO;
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
    private Vector3 _recoilRotation = new Vector3();

    private void Start()
    {
        _weaponAim.OnPlayerFiredWithAim += ActivateRecoilInAim;
        _weaponAim.OnPlayerFiredWithoutAim += ActivateRecoilWithoutAim;
        _weaponSlot.OnWeaponChanged += SetWeaponHandler;
    }

    private void Update()
    {
        if (_weapon_SO == null) { return; }

        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _weapon_SO.recoilReturnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_targetRotation, _targetRotation, _weapon_SO.snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    private void ActivateRecoilInAim()
    {
        SetRecoil(_weapon_SO.recoil);
    }

    private void ActivateRecoilWithoutAim()
    {
        SetRecoil(_weapon_SO.aimRecoil);
    }

    private void SetRecoil(Vector3 recoilRotation)
    {
        _recoilRotation.x = recoilRotation.x;
        _recoilRotation.y = Random.Range(-recoilRotation.y, recoilRotation.y);
        _recoilRotation.z = Random.Range(-recoilRotation.z, recoilRotation.z);

        _targetRotation += _recoilRotation;
    }

    private void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weapon_SO = weaponHandler.Weapon_SO;
    }

    private void OnDestroy()
    {
        _weaponAim.OnPlayerFiredWithAim += ActivateRecoilInAim;
        _weaponAim.OnPlayerFiredWithoutAim += ActivateRecoilWithoutAim;
        _weaponSlot.OnWeaponChanged -= SetWeaponHandler;
    }
}
