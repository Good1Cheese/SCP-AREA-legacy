using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponNoAmmo), typeof(WeaponAim))]
public class WeaponFire : WeaponScriptBase
{
    private const KeyCode FIRE_KEY = KeyCode.Mouse0;

    private RayForFireProvider _rayForFireProvider;
    private WeaponAim _weaponAim;
    private WeaponShot _weaponShot;
    private WeaponNoAmmo _weaponNoAmmo;
    private WeaponReload _weaponReload;

    public override WaitForSeconds RequestTimeout => _weaponHandler.Weapon_SO.shotTimeout;

    public override AudioClip RequestClip => _weaponHandler.Weapon_SO.shotSound;

    [Inject]
    private void Inject(RayForFireProvider rayForFireProvider,
                        WeaponAim weaponAim,
                        WeaponShot weaponShot,
                        WeaponNoAmmo weaponMiss,
                        WeaponReload weaponReload)
    {
        _rayForFireProvider = rayForFireProvider;
        _weaponAim = weaponAim;
        _weaponShot = weaponShot;
        _weaponNoAmmo = weaponMiss;
        _weaponReload = weaponReload;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(FIRE_KEY)) { return; }

        if (CanNotWeaponDoAction()) { return; }

        if (_weaponReload.CurrentClipAmmo == 0)
        {
            _weaponNoAmmo.ShootWithNoAmmo();
            return;
        }

        _weaponRequestsHandler.Handle(this);
    }

    private void Fire()
    {
        _weaponReload.CurrentClipAmmo--;

        Physics.Raycast(_rayForFireProvider.ProvideRay(), out RaycastHit raycastHit);
        _weaponShot.Shoot(raycastHit);

        if (_weaponAim.IsAiming)
        {
            _weaponAim.FiredWithAim?.Invoke();
            return;
        }
        _weaponAim.FiredWithoutAim?.Invoke();
    }

    public override void Interact() => Fire();
}