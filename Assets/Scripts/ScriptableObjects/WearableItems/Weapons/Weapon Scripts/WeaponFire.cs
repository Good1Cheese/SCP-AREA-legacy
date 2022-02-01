using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponNoAmmo), typeof(WeaponAim))]
public class WeaponFire : WeaponScriptBase, IInteractable
{
    private const KeyCode FIRE_KEY = KeyCode.Mouse0;

    private RayForFireProvider _rayForFireProvider;
    private WeaponAim _weaponAim;
    private WeaponShot _weaponShot;
    private WeaponNoAmmo _weaponNoAmmo;
    private WeaponReload _weaponReload;

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

        if (_weaponHandler.ClipAmmo == 0)
        {
            _weaponNoAmmo.ShootWithNoAmmo();
            return;
        }

        _weaponRequestsHandler.Handle(this, _weaponHandler.Weapon_SO.shotTimeout);
    }

    private void Fire()
    {
        _weaponReload.CurrentClip.Item.Ammo--;

        Physics.Raycast(_rayForFireProvider.ProvideRay(), out RaycastHit raycastHit);
        _weaponShot.Shoot(raycastHit);

        if (_weaponAim.IsAiming)
        {
            _weaponAim.FiredWithAim?.Invoke();
            return;
        }
        _weaponAim.FiredWithoutAim?.Invoke();
    }

    public void Interact() => Fire();
}