using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponShotSound), typeof(WeaponMiss), typeof(WeaponAim))]
public class WeaponFire : WeaponAction
{
    const KeyCode FIRE_KEY = KeyCode.Mouse0;

    [Inject] readonly RayForShootingProvider m_rayForShootingProvider;

    public Action OnPlayerShooted { get; set; }

    WeaponMiss m_weaponMiss;

    void Awake()
    {
        m_weaponMiss = GetComponent<WeaponMiss>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(FIRE_KEY) || m_wearableItemsInventory.WeaponSlot.IsItemActionGoing) { return; }

        if (m_weaponHandler.ClipAmmo == 0) 
        {
           m_weaponMiss.ActivateMissSound();
            return;
        }

        Shoot();
    }

    void Shoot()
    {
        m_weaponHandler.ClipAmmo--;
        OnPlayerShooted?.Invoke();

        m_wearableItemsInventory.WeaponSlot.StartItemAction(m_weaponHandler.Weapon_SO.shotTimeout);

        if (!Physics.Raycast(m_rayForShootingProvider.ProvideRay(), out RaycastHit raycastHit)) { return; }

        m_rayForShootingProvider.OnRayLaunched.Invoke(raycastHit);
    }
}
