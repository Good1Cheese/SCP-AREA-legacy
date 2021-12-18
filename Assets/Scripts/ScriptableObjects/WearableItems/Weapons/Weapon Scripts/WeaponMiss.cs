using System;
using UnityEngine;
using Zenject;

public class WeaponMiss : MonoBehaviour
{
    [Inject] private readonly ItemActionCreator _itemActionCreator;
    [Inject] private readonly WeaponSlot _weaponSlot;

    private WaitForSeconds _timeoutAfterShot;
    private WeaponHandler _weaponHandler;

    private void Start()
    {
        _weaponSlot.Changed += SetWeaponTimeoutAfterShot;
    }

    public void ActivateMissSound()
    {
        _itemActionCreator.StartItemAction(_timeoutAfterShot, _weaponHandler.Weapon_SO.missFireSound, false);
    }

    private void SetWeaponTimeoutAfterShot(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
        _timeoutAfterShot = new WaitForSeconds(weaponHandler.Weapon_SO.shotDelay);
    }

    private void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponTimeoutAfterShot;
    }
}