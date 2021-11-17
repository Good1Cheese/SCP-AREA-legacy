using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponMissFireSound))]
public class WeaponMiss : MonoBehaviour
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;
    private WaitForSeconds _timeoutAfterShot;

    public Action OnAmmoRunOut { get; set; }

    private void Start()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeaponTimeoutAfterShot;
    }

    public void ActivateMissSound()
    {
        _wearableItemsInventory.WeaponSlot.StartItemAction(_timeoutAfterShot);

        OnAmmoRunOut.Invoke();
    }

    private void SetWeaponTimeoutAfterShot(WeaponHandler weaponHandler)
    {
        _timeoutAfterShot = new WaitForSeconds(weaponHandler.Weapon_SO.shotDelay);
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeaponTimeoutAfterShot;
    }
}