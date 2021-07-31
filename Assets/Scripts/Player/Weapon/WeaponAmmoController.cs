using System;
using System.Collections;
using UnityEngine;

public class WeaponAmmoController : WeaponAction
{
    WaitForSeconds m_timeoutAfterAction;

    public Action OnAmmoRunOut { get; set; }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || m_currentGun_SO.cartridgeСlipAmmo != 0) { return; }

        m_equipmentInventory.WeaponCell.WeaponAction = ActivateMissSound();
    }

    IEnumerator ActivateMissSound()
    {
        OnAmmoRunOut.Invoke();
        yield return m_timeoutAfterAction;

        m_equipmentInventory.WeaponCell.WeaponAction = null;
    }

    protected override void SetWeapon(Weapon_SO weapon)
    {
        base.SetWeapon(weapon);
        m_timeoutAfterAction = new WaitForSeconds(weapon.missFireSound.length);
    }

}