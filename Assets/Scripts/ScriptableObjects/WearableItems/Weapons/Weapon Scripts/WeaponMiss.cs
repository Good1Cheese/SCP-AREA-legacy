using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponMissFireSound))]
public class WeaponMiss : MonoBehaviour
{
    [Inject] private readonly WeaponSlot _weaponSlot;

    private WaitForSeconds _timeoutAfterShot;

    public Action OnAmmoRunOut { get; set; }

    private void Start()
    {
        _weaponSlot.OnWeaponChanged += SetWeaponTimeoutAfterShot;
    }

    public void ActivateMissSound()
    {
        _weaponSlot.StartItemAction(_timeoutAfterShot);

        OnAmmoRunOut.Invoke();
    }

    private void SetWeaponTimeoutAfterShot(WeaponHandler weaponHandler)
    {
        _timeoutAfterShot = new WaitForSeconds(weaponHandler.Weapon_SO.shotDelay);
    }

    private void OnDestroy()
    {
        _weaponSlot.OnWeaponChanged -= SetWeaponTimeoutAfterShot;
    }
}