using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponShotSound), typeof(WeaponMiss), typeof(WeaponAim))]
public class WeaponFire : WeaponAction
{
    const KeyCode FIRE_KEY = KeyCode.Mouse0;

    [Inject] readonly RayForFireProvider m_rayForFireProvider;
    [Inject] readonly WeaponAim m_weaponAim;
    [Inject] readonly WeaponShot m_weaponShot;

    public Action OnPlayerFired { get; set; }

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
        m_wearableItemsInventory.WeaponSlot.StartItemAction(m_weaponHandler.Weapon_SO.shotTimeout);
        m_weaponHandler.ClipAmmo--;

        OnPlayerFired?.Invoke();


        Physics.Raycast(m_rayForFireProvider.ProvideRay(), out RaycastHit raycastHit);
        m_weaponShot.AttendShot(raycastHit);

        if (m_weaponAim.IsAiming)
        {
            m_weaponAim.OnPlayerFiredWithAim?.Invoke();
            return;
        }
        m_weaponAim.OnPlayerFiredWithoutAim?.Invoke();
    }
}
