using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadSound))]
public class WeaponReload : WeaponAction
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    public bool IsPlayerReloading { get; set; }
    public Action OnPlayerReloaded { get; set; }

    public void ActivateReload()
    {
        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || _weaponHandler.AmmoCount == 0
            || _wearableItemsInventory.WeaponSlot.IsItemActionGoing) { return; }

        IsPlayerReloading = true;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _wearableItemsInventory.WeaponSlot.StartItemAction(_weaponHandler.Weapon_SO.reloadTimeout);

        AmmoHandler ammoHandler = (AmmoHandler)_pickableItemsInventory.Inventory.TakeWhile(item => item != null).LastOrDefault(item => item as AmmoHandler != null);
        int ammoToReload = GetAmmoToRelod();

        if (ammoHandler == null) { yield break; }

        _weaponHandler.ClipAmmo = 0;
        ammoHandler.AmmoCount -= ammoToReload;

        OnPlayerReloaded?.Invoke();

        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        _weaponHandler.ClipAmmo = ammoToReload;
        IsPlayerReloading = false;
    }

    private int GetAmmoToRelod()
    {
        int ammoCount = _weaponHandler.AmmoCount;

        if (ammoCount >= _weaponHandler.Weapon_SO.clipMaxAmmo)
        {
            return _weaponHandler.Weapon_SO.clipMaxAmmo;
        }

        return ammoCount;
    }
}
