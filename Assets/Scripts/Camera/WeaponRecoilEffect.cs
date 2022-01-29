using UnityEngine;
using Zenject;

public class WeaponRecoilEffect : MonoBehaviour
{
    private WeaponAim _weaponAim;
    private WeaponSlot _weaponSlot;

    private Weapon_SO _weapon_SO;
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
    private Vector3 _recoilRotation = new Vector3();

    [Inject]
    private void Construct(WeaponAim weaponAim, WeaponSlot weaponSlot)
    {
        _weaponAim = weaponAim;
        _weaponSlot = weaponSlot;
    }

    private void Start()
    {
        _weaponAim.FiredWithAim += ActivateRecoilInAim;
        _weaponAim.FiredWithoutAim += ActivateRecoilWithoutAim;
        _weaponSlot.Changed += SetWeaponHandler;
    }

    private void Update()
    {
        if (_weapon_SO == null) { return; }

        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _weapon_SO.recoilReturnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_targetRotation, _targetRotation, _weapon_SO.snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    private void ActivateRecoilInAim() => GenerateRecoil(_weapon_SO.recoil);
    private void ActivateRecoilWithoutAim() => GenerateRecoil(_weapon_SO.aimRecoil);

    private void GenerateRecoil(Vector3 recoilRotation)
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
        _weaponAim.FiredWithAim -= ActivateRecoilInAim;
        _weaponAim.FiredWithoutAim -= ActivateRecoilWithoutAim;
        _weaponSlot.Changed -= SetWeaponHandler;
    }
}