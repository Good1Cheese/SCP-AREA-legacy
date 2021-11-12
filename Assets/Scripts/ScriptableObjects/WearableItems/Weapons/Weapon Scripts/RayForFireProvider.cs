﻿using UnityEngine;
using Zenject;

public class RayForFireProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] float m_multyplierOfBulletSpawnPointRadious;
    [SerializeField] Transform m_bulletSpawnPoint;

    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponAim m_weaponAim;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    Ray ray;

    void Awake()
    {
        ray = new Ray();
    }

    void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeaponBulletSpawnPoint;
    }

    public Ray ProvideRay()
    {
        if (m_weaponAim.IsAiming)
        {
            return ray = ProvideRayForAimedShot();
        }
        else
        {
            return ray = ProvideRayForShot();
        }
    }

    public Ray ProvideRayForAimedShot()
    {
        return ProvideRay(m_bulletSpawnPoint.position);
    }

    public Ray ProvideRayForShot()
    {
        Vector3 origin = m_playerTransform.position + m_playerTransform.up + (Vector3)Random.insideUnitCircle * m_multyplierOfBulletSpawnPointRadious;
        return ProvideRay(origin);
    }

    public Ray ProvideRay(Vector3 origin)
    {
        ray.origin = origin;
        ray.direction = transform.forward;
        return ray;
    }

    void SetWeaponBulletSpawnPoint(WeaponHandler weaponHandler)
    {
        m_bulletSpawnPoint.localPosition = weaponHandler.Weapon_SO.bulletSpawnPoint;
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeaponBulletSpawnPoint;
    }

}