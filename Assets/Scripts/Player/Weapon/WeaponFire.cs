using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponShotSound), typeof(IRayProvider), typeof(WeaponAim))]
public class WeaponFire : WeaponAction
{
    [Inject] readonly IRayProvider rayForShootingProvider;

    WaitForSeconds m_timeoutAfterShooting;

    public Action OnPlayerShooted { get; set; }
    public Action OnPlayerShootedWithAim { get; set; }
    public Action OnPlayerShootedWithoutAim { get; set; }
    public Action<RaycastHit> OnRayLaunched { get; set; }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || m_currentGun_SO.cartridge—lipAmmo <= 0) { return; }

        if (m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing) { return; }
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing = true;

        if (!Physics.Raycast(rayForShootingProvider.ProvideRay(), out RaycastHit raycastHit)) { yield break; }

        m_currentGun_SO.cartridge—lipAmmo--;
        OnPlayerShooted.Invoke();
        OnRayLaunched.Invoke(raycastHit);

        yield return m_timeoutAfterShooting;

        m_equipmentInventory.WeaponSlot.IsWeaponActionIsGoing = false;
    }


    protected override void SetWeapon(Weapon_SO weapon)
    {
        base.SetWeapon(weapon);
        m_timeoutAfterShooting = new WaitForSeconds(weapon.shotSound.length);
    }

}
