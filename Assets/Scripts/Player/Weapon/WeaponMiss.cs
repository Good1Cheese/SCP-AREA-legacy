using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WeaponMissFireSound))]
public class WeaponMiss : WeaponAction
{
    WaitForSeconds m_timeoutAfterAction;

    public Action OnAmmoRunOut { get; set; }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || m_currentGun_SO.cartridgeСlipAmmo != 0) { return; }

        if (m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }
        StartCoroutine(ActivateMissSound());
    }

    IEnumerator ActivateMissSound()
    {
        m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        OnAmmoRunOut.Invoke();
        yield return m_timeoutAfterAction;

        m_equipmentInventory.WeaponSlot.WeaponAction = null;

        m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }

    protected override void SetWeapon(Weapon_SO weapon)
    {
        base.SetWeapon(weapon);
        m_timeoutAfterAction = new WaitForSeconds(weapon.missFireSound.length);
    }

}