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

    public override WaitForSeconds InteractionTimeout => _weaponHandler.Weapon_SO.shotTimeout;

    public override AudioClip Sound => _weaponHandler.Weapon_SO.shotSound;

    [Inject]
    private void Inject(RayForFireProvider rayForFireProvider,
                        WeaponAim weaponAim,
                        WeaponShot weaponShot,
                        WeaponNoAmmo weaponMiss)
    {
        _rayForFireProvider = rayForFireProvider;
        _weaponAim = weaponAim;
        _weaponShot = weaponShot;
        _weaponNoAmmo = weaponMiss;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(FIRE_KEY)) { return; }

        if (IsWeaponNotAvailable()) { return; }

        if (_weaponHandler.CurrentClipAmmo == 0)
        {
            _weaponNoAmmo.ShootWithNoAmmo();
            return;
        }

        _weaponRequestsHandler.Handle(this);
    }

    private void Fire()
    {
        _weaponHandler.CurrentClipAmmo--;

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