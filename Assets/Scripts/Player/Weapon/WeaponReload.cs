using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponReloadSound))]
public class WeaponReload : WeaponAction
{
    [Inject] readonly PickableItemsInventory m_pickableItemsInventory;
    WaitForSeconds m_timeoutAfterReload;

    public Action OnPlayerReloaded { get; set; }
    public Action OnWeaponAmmoChanged { get; set; }

    void Update()
    {
        if (Input.GetButtonDown("Reload"))
        {
            if (m_weaponHandler.ClipAmmo == m_weaponHandler.Weapon_SO.clipMaxAmmo
                || m_weaponHandler.AmmoCount <= 0) { return; }

            if (m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

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

        yield return m_timeoutAfterReload;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }

    public void UpdateWeaponAmmoCount(int droppedAmmoCount)
    {
        if (m_weaponHandler == null) { return; }

        m_weaponHandler.AmmoCount -= droppedAmmoCount;
        OnWeaponAmmoChanged?.Invoke();
    }


    protected override void SetWeapon(WeaponHandler weaponHandler)
    {
        base.SetWeapon(weaponHandler);
        m_timeoutAfterReload = new WaitForSeconds(weaponHandler.Weapon_SO.reloadSoundPrefab.length);
    }

}
