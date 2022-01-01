using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadCoroutineUser))]
public class WeaponReload : WeaponScriptBase
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly WeaponReloadCoroutineUser _weaponReloadCoroutineUser;
    [Inject] private readonly ItemActionCreator _itemActionCreator;

    private IEnumerable<AmmoHandler> _inventoryAmmoEnumarable;
    private int _calculatedClipAmmo;

    public Action Reloaded { get; set; }

    public void ActivateReload()
    {
        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || _weaponHandler.Ammo == 0
            || _itemActionCreator.IsGoing
            || _pauseMenuEnablerDisabler.IsActivated) { return; }

        _weaponReloadCoroutineUser.StartWithoutInterrupt();
    }

    public IEnumerator Reload()
    {
        _itemActionCreator.StartInterruptingItemAction(_weaponReloadCoroutineUser, _weaponHandler.Weapon_SO.reloadSound);

        AddClipAmmoToInventoryAmmo();
        _weaponHandler.ClipAmmo = 0;

        Reloaded?.Invoke();

        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        CalculateCurrentClipAmmo();

        _weaponHandler.ClipAmmo = _calculatedClipAmmo;
        _weaponReloadCoroutineUser.IsCoroutineGoing = false;
    }

    private void CalculateCurrentClipAmmo()
    {
        AmmoHandler[] ammos = GetInventoryAmmo();
        int clipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo;

        for (int i = 0; i < ammos.Length; i++)
        {
            var currentAmmo = ammos[i].Ammo;
            ammos[i].Ammo -= clipAmmo;

            if (currentAmmo > clipAmmo) { clipAmmo = 0; break; }

            clipAmmo -= currentAmmo;

            if (clipAmmo < 0) { break; }
        }
        _calculatedClipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo - clipAmmo;
    }

    private AmmoHandler[] GetInventoryAmmo()
    {
        return _inventoryAmmoEnumarable.ToArray();
    }

    private void AddClipAmmoToInventoryAmmo()
    {
        _inventoryAmmoEnumarable = _pickableItemsInventory.Inventory.TakeWhile(item => item != null).Where(item =>
        {
            var ammo = item as AmmoHandler;
            return ammo != null && ammo.Ammo != 0;
        }).Select(item => (AmmoHandler)item);

        var ammo = _inventoryAmmoEnumarable.FirstOrDefault(ammo => ammo.Ammo + _weaponHandler.ClipAmmo <= AmmoHandler.MAX_SLOT_AMMO);
        ammo.Ammo += _weaponHandler.ClipAmmo;
    }
}