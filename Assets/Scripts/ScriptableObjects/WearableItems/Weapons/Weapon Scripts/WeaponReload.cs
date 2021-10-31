using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadSound))]
public class WeaponReload : WeaponAction
{
    const KeyCode RELOAD_KEY = KeyCode.R;

    [Inject] readonly PickableItemsInventory m_pickableItemsInventory;
    [Inject] readonly WeaponAim m_weaponAim;

    public bool IsPlayerReloading { get; set; }
    public Action OnPlayerReloaded { get; set; }
    public Action OnWeaponAmmoChanged { get; set; }

    void Update()
    {
        if (Input.GetKeyDown(RELOAD_KEY))
        {
            if (m_weaponHandler.ClipAmmo == m_weaponHandler.Weapon_SO.clipMaxAmmo
                || m_weaponHandler.AmmoCount <= 0) { return; }

            if (m_wearableItemsInventory.WeaponSlot.IsItemActionGoing) { return; }

            IsPlayerReloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        m_weaponAim.SetAimState(false);

        m_wearableItemsInventory.WeaponSlot.StartItemAction(m_weaponHandler.Weapon_SO.reloadTimeout);

        AmmoHandler ammoHandler = (AmmoHandler)m_pickableItemsInventory.Inventory.LastOrDefault(item => item as AmmoHandler != null);

        int ammoToReload = (m_weaponHandler.AmmoCount >= m_weaponHandler.Weapon_SO.clipMaxAmmo)
            ? m_weaponHandler.Weapon_SO.clipMaxAmmo
            : m_weaponHandler.AmmoCount;

        m_weaponHandler.ClipAmmo = ammoToReload;
        m_weaponHandler.AmmoCount -= ammoToReload;

        if (ammoHandler != null)
        {
            ammoHandler.AmmoCount -= ammoToReload;
        }

        OnPlayerReloaded?.Invoke();
        OnWeaponAmmoChanged?.Invoke();

        yield return m_weaponHandler.Weapon_SO.reloadTimeout;

        IsPlayerReloading = false;
    }

    public void UpdateWeaponAmmoCount(int droppedAmmoCount)
    {
        if (m_weaponHandler == null) { return; }

        m_weaponHandler.AmmoCount -= droppedAmmoCount;
        OnWeaponAmmoChanged?.Invoke();
    }
}
