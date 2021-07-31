using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponShotSound), typeof(IRayProvider), typeof(WeaponAim))]
public class WeaponFire : WeaponAction
{
    [Inject] readonly IRayProvider rayForShootingProvider;

    WaitForSeconds m_timeoutAfterShooting;
    IDamagable m_damagable;

    public Action OnPlayerShooted { get; set; }
    public Action OnPlayerShootedWithAim { get; set; }
    public Action OnPlayerShootedWithoutAim { get; set; }


    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || m_currentGun_SO.cartridge—lipAmmo <= 0) { return; }

        m_equipmentInventory.WeaponCell.WeaponAction = Shoot();
    }

    IEnumerator Shoot()
    {
        if (!Physics.Raycast(rayForShootingProvider.ProvideRay(), out RaycastHit raycastHit)) { yield break; }

        bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out m_damagable);
        m_currentGun_SO.cartridge—lipAmmo--;
        OnPlayerShooted.Invoke();

        if (isHitObjectInteractable)
        {
            m_damagable.Damage(m_currentGun_SO.damagePerShot);
        }

        yield return m_timeoutAfterShooting;

        m_equipmentInventory.WeaponCell.WeaponAction = null;
    }


    protected override void SetWeapon(Weapon_SO weapon)
    {
        base.SetWeapon(weapon);
        m_timeoutAfterShooting = new WaitForSeconds(weapon.shotSound.length);
    }

}
