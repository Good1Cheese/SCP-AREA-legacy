using UnityEngine;
using Zenject;

public class WeaponShot : MonoBehaviour
{
    [Inject] readonly WeaponFire m_weaponFire;
    [Inject] readonly EquipmentInventory m_equipmentInventory;

    IDamagable m_damagable;
    Weapon_SO m_weapon;

    void Start()
    {
        m_weaponFire.OnRayLaunched += AttendShot;
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    private void AttendShot(RaycastHit raycastHit)
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

    void SetWeapon(Weapon_SO weapon)
    {
        m_weapon = weapon;
    }


    void OnDestroy()
    {
        m_weaponFire.OnRayLaunched -= AttendShot;
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
    }
}