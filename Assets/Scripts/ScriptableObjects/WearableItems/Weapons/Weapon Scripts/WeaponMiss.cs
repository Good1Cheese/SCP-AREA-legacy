using System;
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

    public void ActivateMissSound()
    {
        m_wearableItemsInventory.WeaponSlot.StartItemAction(m_timeoutAfterShot);

        OnAmmoRunOut.Invoke();
    }

    void SetWeaponTimeoutAfterShot(WeaponHandler weaponHandler)
    {
        m_timeoutAfterShot = new WaitForSeconds(weaponHandler.Weapon_SO.shotDelay);
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeaponTimeoutAfterShot;
    }
}