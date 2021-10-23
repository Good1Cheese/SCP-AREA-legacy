using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponMissFireSound))]
public class WeaponMiss : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    WaitForSeconds m_timeoutAfterShot;

    public Action OnAmmoRunOut { get; set; }

    void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeaponTimeoutAfterShot;
    }

    public IEnumerator ActivateMissSound()
    {
        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        OnAmmoRunOut.Invoke();
        yield return m_timeoutAfterShot;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }

    void SetWeaponTimeoutAfterShot(WeaponHandler weaponHandler)
    {
        m_timeoutAfterShot = new WaitForSeconds(weaponHandler.Weapon_SO.delayAfterShot);
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeaponTimeoutAfterShot;
    }
}