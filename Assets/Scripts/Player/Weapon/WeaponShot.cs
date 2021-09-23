using UnityEngine;
using Zenject;

public class WeaponShot : MonoBehaviour
{
    [Inject] readonly RayForShootingProvider m_rayForShootingProvider;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    IDamagable m_damagable;
    Weapon_SO m_weapon;

    void Start()
    {
        m_rayForShootingProvider.OnRayLaunched += AttendShot;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    void AttendShot(RaycastHit raycastHit)
    {
        bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out m_damagable);
        if (isHitObjectInteractable)
        {
            m_damagable.Damage(m_weapon.damagePerShot);
            return;
        }

        Vector3 highestPointOfCollider = raycastHit.point + raycastHit.normal * 0.001f;
        GameObject bulletHole = Instantiate(m_weapon.bulletHolePrefab, highestPointOfCollider, Quaternion.identity);
        bulletHole.transform.LookAt(raycastHit.point + raycastHit.normal);

    }

    void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weapon = weaponHandler.Weapon_SO;
    }


    void OnDestroy()
    {
        m_rayForShootingProvider.OnRayLaunched -= AttendShot;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
    }
}