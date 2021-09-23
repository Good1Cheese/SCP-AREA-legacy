using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WeaponReloadSound))]
public class WeaponReload : WeaponAction
{
    WaitForSeconds m_timeoutAfterReload;

    public Action OnPlayerReloaded { get; set; }

    void Update()
    {
        if (Input.GetButtonDown("Reload"))
        {
            if (m_weaponHandler.ClipAmmo == m_weaponHandler.Weapon_SO.clipMaxAmmo
                || m_weaponHandler.AmmoCount == 0) { return; }

            if (m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        int ammoToReload = (m_weaponHandler.AmmoCount >= m_weaponHandler.Weapon_SO.clipMaxAmmo)
            ? m_weaponHandler.Weapon_SO.clipMaxAmmo
            : m_weaponHandler.AmmoCount;

        m_weaponHandler.ClipAmmo = ammoToReload;
        m_weaponHandler.AmmoCount -= ammoToReload;

        OnPlayerReloaded.Invoke();

        yield return m_timeoutAfterReload;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }


    protected override void SetWeapon(WeaponHandler weaponHandler)
    {
        base.SetWeapon(weaponHandler);
        m_timeoutAfterReload = new WaitForSeconds(weaponHandler.Weapon_SO.reloadSoundPrefab.length);
    }

}
