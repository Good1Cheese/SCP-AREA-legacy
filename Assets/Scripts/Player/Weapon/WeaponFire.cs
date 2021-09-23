using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponShotSound), typeof(WeaponMiss), typeof(WeaponAim))]
public class WeaponFire : WeaponAction
{
    [Inject] readonly RayForShootingProvider m_rayForShootingProvider;

    public Action OnPlayerShooted { get; set; }

    WeaponMiss m_weaponMiss;

    void Awake()
    {
        m_weaponMiss = GetComponent<WeaponMiss>();
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }

        if (m_weaponHandler.ClipAmmo == 0) 
        {
            StartCoroutine(m_weaponMiss.ActivateMissSound());
            return;
        }

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        m_weaponHandler.ClipAmmo--;
        OnPlayerShooted?.Invoke();

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        if (!Physics.Raycast(m_rayForShootingProvider.ProvideRay(), out RaycastHit raycastHit)) { yield break; }

        m_rayForShootingProvider.OnRayLaunched.Invoke(raycastHit);

        yield return m_weaponHandler.Weapon_SO.timeoutAfterShot;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }
    
    protected override void SetWeapon(WeaponHandler weaponHandler)
    {
        base.SetWeapon(weaponHandler);
    }
}
