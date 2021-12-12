using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponMiss), typeof(WeaponAim))]
public class WeaponFire : WeaponAction
{
    private const KeyCode FIRE_KEY = KeyCode.Mouse0;

    [Inject] private readonly RayForFireProvider _rayForFireProvider;
    [Inject] private readonly WeaponAim _weaponAim;
    [Inject] private readonly WeaponShot _weaponShot;
    [Inject] private readonly WeaponMiss _weaponMiss;

    public Action OnPlayerFired { get; set; }

    private void Update()
    {
        if (!Input.GetKeyDown(FIRE_KEY) || _weaponSlot.ItemActionMaker.IsItemActionGoing) { return; }

        if (_weaponHandler.ClipAmmo == 0)
        {
            _weaponMiss.ActivateMissSound();
            return;
        }

        Fire();
    }

    private void Fire()
    {
        _weaponSlot.ItemActionMaker.StartItemAction(_weaponHandler.Weapon_SO.shotTimeout, _weaponHandler.CurrentShotSound);
        _weaponHandler.ClipAmmo--;

        OnPlayerFired?.Invoke();

        Physics.Raycast(_rayForFireProvider.ProvideRay(), out RaycastHit raycastHit);
        _weaponShot.Shoot(raycastHit);

        if (_weaponAim.IsAiming)
        {
            _weaponAim.OnPlayerFiredWithAim?.Invoke();
            return;
        }
        _weaponAim.OnPlayerFiredWithoutAim?.Invoke();
    }
}