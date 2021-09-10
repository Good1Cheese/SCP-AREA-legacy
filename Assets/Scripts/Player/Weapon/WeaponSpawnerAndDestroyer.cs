using UnityEngine;
using Zenject;

public class WeaponSpawnerAndDestroyer : MonoBehaviour
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    [Inject] readonly Transform m_playerTransform;

    Weapon_SO m_weapon;
    GameObject m_spawnedGun;
    Transform m_transform;

    public GameObject CurrentGunGameObject { get => m_spawnedGun; }

    void Start()
    {
        m_transform = transform;
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += DestroyGun;
    }

    public void SpawnWeapon(Weapon_SO weapon_SO)
    {
        m_weapon = weapon_SO;
        if (m_weapon.playerWeapon == null)
        {
            m_weapon.playerWeapon = Instantiate(weapon_SO.weaponForPlayerPrefab, transform);
        }
        m_spawnedGun = m_weapon.playerWeapon;
        m_spawnedGun.SetActive(false);
    }

    public void DespawnWeapon()
    {
        m_spawnedGun = null;
        m_weapon.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        m_weapon.gameObject.SetActive(true);
    }

    public void DestroyGun()
    {
        Destroy(m_spawnedGun);
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= DestroyGun;
    }
}
