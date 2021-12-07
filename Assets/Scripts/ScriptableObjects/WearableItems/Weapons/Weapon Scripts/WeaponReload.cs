using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadSound))]
public class WeaponReload : WeaponAction
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    private IEnumerable<ItemHandler> _items;
    private int _calculatedClipAmmo;

    public bool IsReloading { get; set; }
    public Action OnReloadStarted { get; set; }

    public void ActivateReload()
    {
        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || _weaponHandler.Ammo == 0
            || _weaponSlot.IsItemActionGoing) { return; }

        IsReloading = true;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _weaponSlot.StartItemAction(_weaponHandler.Weapon_SO.reloadTimeout);

        CalculateCurrentAmmo();
        _weaponHandler.ClipAmmo = 0;

        OnReloadStarted?.Invoke();

        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        _weaponHandler.ClipAmmo = _calculatedClipAmmo;
        IsReloading = false;
    }

    private void CalculateCurrentAmmo()
    {
        _items = _pickableItemsInventory.Inventory.TakeWhile(item => item != null);

        AmmoHandler ammoHandler = GetLastFullAmmoHandler();
        int clipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo;
        _calculatedClipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo;

        if (ammoHandler.Ammo - clipAmmo >= 0)
        {
            ammoHandler.Ammo -= clipAmmo;
            return;
        }

        while (clipAmmo >= ammoHandler.Ammo)
        {
            clipAmmo -= ammoHandler.Ammo;
            ammoHandler.Ammo = 0;
            _pickableItemsInventory.Remove(ammoHandler);

            ammoHandler = GetLastFullAmmoHandler();

            if (ammoHandler != null) { continue; }

            if (clipAmmo != 0)
            {
                _calculatedClipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo - clipAmmo;
            }

            return;
        }
    }

    private AmmoHandler GetLastFullAmmoHandler()
    {
        return (AmmoHandler)_items.LastOrDefault(item =>
        {
            AmmoHandler ammoHandler = item as AmmoHandler;
            return ammoHandler != null && ammoHandler.Ammo != 0;
        });
    }
}