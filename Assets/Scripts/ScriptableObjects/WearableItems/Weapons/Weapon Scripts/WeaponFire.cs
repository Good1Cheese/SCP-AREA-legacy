using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponMiss), typeof(WeaponAim))]
public class WeaponFire : WeaponScriptBase
{
    private const KeyCode FIRE_KEY = KeyCode.Mouse0;

    private RayForFireProvider _rayForFireProvider;
    private WeaponAim _weaponAim;
    private WeaponShot _weaponShot;
    private WeaponMiss _weaponMiss;
    private ItemActionCreator _itemActionCreator;

    public Action Fired { get; set; }

    [Inject]
    private void Inject(RayForFireProvider rayForFireProvider,
                        WeaponAim weaponAim,
                        WeaponShot weaponShot,
                        WeaponMiss weaponMiss,
                        ItemActionCreator itemActionCreator)
    {
        _rayForFireProvider = rayForFireProvider;
        _weaponAim = weaponAim;
        _weaponShot = weaponShot;
        _weaponMiss = weaponMiss;
        _itemActionCreator = itemActionCreator;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(FIRE_KEY)) { return; }

        if (_itemActionCreator.IsGoing
            || CanNotWeaponDoAction()) { return; }

        if (_weaponHandler.ClipAmmo == 0)
        {
            _weaponMiss.ActivateMissSound();
            return;
        }

        Fire();
    }

    private void Fire()
    {
        _itemActionCreator.StartItemAction(_weaponHandler.Weapon_SO.shotTimeout, _weaponHandler.CurrentShotSound, false);
        _weaponHandler.ClipAmmo--;

        Fired?.Invoke();

        Physics.Raycast(_rayForFireProvider.ProvideRay(), out RaycastHit raycastHit);
        _weaponShot.Shoot(raycastHit);

        if (_weaponAim.IsAiming)
        {
            _weaponAim.FiredWithAim?.Invoke();
            return;
        }
        _weaponAim.FiredWithoutAim?.Invoke();
    }
}