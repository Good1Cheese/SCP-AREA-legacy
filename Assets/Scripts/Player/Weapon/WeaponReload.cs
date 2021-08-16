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
            if (m_currentGun_SO.cartridgeСlipAmmo == m_currentGun_SO.cartidgeClipMaxAmmo
                || m_currentGun_SO.ammoCount == 0) { return; }

            if (m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        int ammoToReload = (m_currentGun_SO.ammoCount >= m_currentGun_SO.cartidgeClipMaxAmmo)
            ? m_currentGun_SO.cartidgeClipMaxAmmo
            : m_currentGun_SO.ammoCount;

        m_currentGun_SO.cartridgeСlipAmmo = ammoToReload;
        m_currentGun_SO.ammoCount -= ammoToReload;

        OnPlayerReloaded.Invoke();

        yield return m_timeoutAfterReload;

        m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }


    protected override void SetWeapon(Weapon_SO weapon)
    {
        base.SetWeapon(weapon);
        m_timeoutAfterReload = new WaitForSeconds(weapon.reloadSound.length);
    }

}
