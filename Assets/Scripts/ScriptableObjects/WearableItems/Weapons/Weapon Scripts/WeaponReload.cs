using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadSound), typeof(WeaponReloadCoroutineUser))]
public class WeaponReload : WeaponAction
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly WeaponReloadCoroutineUser _weaponReloadCoroutineUser;

    private IEnumerable<ItemHandler> _items;
    private int _calculatedClipAmmo;

    public Action OnReloadStarted { get; set; }

    public void ActivateReload()
    {
        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || _weaponHandler.Ammo == 0
            || _weaponSlot.IsItemActionGoing) { return; }

        _weaponReloadCoroutineUser.StartAction();
    }

    public IEnumerator Reload()
    {
        _weaponSlot.StartInterruptingItemAction(_weaponHandler.Weapon_SO.reloadTimeout);

        CalculateCurrentAmmo();
        _weaponHandler.ClipAmmo = 0;

        OnReloadStarted?.Invoke();

        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        _weaponHandler.ClipAmmo = _calculatedClipAmmo;
        _weaponReloadCoroutineUser.IsActionGoing = false;
    }

    private void CalculateCurrentAmmo()
    {
        _items = _pickableItemsInventory.Inventory.TakeWhile(item => item != null);

        AmmoHandler[] ammos = GetAmmo();
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

    private AmmoHandler[] GetAmmo()
    {
        return _items.Where(item =>
                 {
                     var ammoHandler = item as AmmoHandler;
                     return ammoHandler != null && ammoHandler.Ammo != 0;
                 }).Select(item => (AmmoHandler)item).ToArray();
    }
}