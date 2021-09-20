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

        if (m_weapon_SO.clipAmmo == 0) 
        {
            print("das");
            StartCoroutine(m_weaponMiss.ActivateMissSound());
            return;
        }

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        m_weapon_SO.clipAmmo--;
        OnPlayerShooted?.Invoke();

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        if (!Physics.Raycast(m_rayForShootingProvider.ProvideRay(), out RaycastHit raycastHit)) { yield break; }

        m_rayForShootingProvider.OnRayLaunched.Invoke(raycastHit);

        yield return m_weapon_SO.timeoutAfterShot;

        m_wearableItemsInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }
    
    protected override void SetWeapon(Weapon_SO weapon_SO)
    {
        base.SetWeapon(weapon_SO);
        weapon_SO.timeoutAfterShot ??= new WaitForSeconds(weapon_SO.delayAfterShot);
    }
}
