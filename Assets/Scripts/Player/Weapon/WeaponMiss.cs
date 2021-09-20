using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponMissFireSound))]
public class WeaponMiss : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    WaitForSeconds m_timeoutAfterAction;

    public Action OnAmmoRunOut { get; set; }

    void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    public IEnumerator ActivateMissSound()
    {
        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        OnAmmoRunOut.Invoke();
        yield return m_timeoutAfterAction;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }

    void SetWeapon(Weapon_SO weapon_SO)
    {
        m_timeoutAfterAction = new WaitForSeconds(weapon_SO.delayAfterShot);
    }

}