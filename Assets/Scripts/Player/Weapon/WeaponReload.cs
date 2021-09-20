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
            if (m_weapon_SO.clipAmmo == m_weapon_SO.clipMaxAmmo
                || m_weapon_SO.ammoCount == 0) { print(m_weapon_SO.clipAmmo == m_weapon_SO.clipMaxAmmo); return; }

            if (m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        int ammoToReload = (m_weapon_SO.ammoCount >= m_weapon_SO.clipMaxAmmo)
            ? m_weapon_SO.clipMaxAmmo
            : m_weapon_SO.ammoCount;

        m_weapon_SO.clipAmmo = ammoToReload;
        m_weapon_SO.ammoCount -= ammoToReload;

        OnPlayerReloaded.Invoke();

        yield return m_timeoutAfterReload;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }


    protected override void SetWeapon(Weapon_SO weapon)
    {
        base.SetWeapon(weapon);
        m_timeoutAfterReload = new WaitForSeconds(weapon.reloadSoundPrefab.length);
    }

}
