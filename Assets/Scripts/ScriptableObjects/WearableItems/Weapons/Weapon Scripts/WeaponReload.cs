using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadSound))]
public class WeaponReload : WeaponAction
{
    private const KeyCode RELOAD_KEY = KeyCode.R;

    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly WeaponAim _weaponAim;

    public bool IsPlayerReloading { get; set; }
    public Action OnPlayerReloaded { get; set; }
    public Action OnWeaponAmmoChanged { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(RELOAD_KEY))
        {
            if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
                || _weaponHandler.AmmoCount == 0
                || _wearableItemsInventory.WeaponSlot.IsItemActionGoing)
            { return; }

            IsPlayerReloading = true;
            _weaponAim.SetAimState(false);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _wearableItemsInventory.WeaponSlot.StartItemAction(_weaponHandler.Weapon_SO.reloadTimeout);

        AmmoHandler ammoHandler = (AmmoHandler)_pickableItemsInventory.Inventory.TakeWhile(item => item != null).LastOrDefault(item => item as AmmoHandler != null);
        int ammoToReload = GetAmmoToRelod();

        _weaponHandler.ClipAmmo = ammoToReload;
        _weaponHandler.AmmoCount -= ammoToReload;

        if (ammoHandler != null)
        {
            ammoHandler.AmmoCount -= ammoToReload;
        }

        OnPlayerReloaded?.Invoke();
        OnWeaponAmmoChanged?.Invoke();

        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        IsPlayerReloading = false;
    }

    private int GetAmmoToRelod()
    {
        if (_weaponHandler.AmmoCount >= _weaponHandler.Weapon_SO.clipMaxAmmo)
        {
            return _weaponHandler.Weapon_SO.clipMaxAmmo;
        }
        return _weaponHandler.AmmoCount;
    }

    public void UpdateWeaponAmmoCount(int droppedAmmoCount)
    {
        if (_weaponHandler == null) { return; }

        _weaponHandler.AmmoCount -= droppedAmmoCount;
        OnWeaponAmmoChanged?.Invoke();
    }
}
