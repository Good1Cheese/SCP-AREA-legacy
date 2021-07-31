using UnityEngine;
using Zenject;

public class WeaponSpawnerAndDestroyer : MonoBehaviour
{ 
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    Weapon_SO m_weapon;
    GameObject m_spawnedGun;

    Transform m_playerTransform;
    
    public GameObject CurrentGunGameObject { get => m_spawnedGun; }

    void Start()
    {
        m_playerTransform = m_equipmentInventory.transform;
        m_equipmentInventory.WeaponCell.OnWeaponChanged += SpawnWeapon;
        m_equipmentInventory.WeaponCell.OnWeaponDropped += DestroyGun;
        m_equipmentInventory.WeaponCell.OnWeaponDropped += SpawnWeapon;
    }

    public void SpawnWeapon(Weapon_SO weapon_SO)
    {
        m_weapon = weapon_SO;
        m_spawnedGun = Instantiate(weapon_SO.weaponForPlayer, transform);
        m_spawnedGun.SetActive(false);
    }

    public void SpawnWeapon()
    {
        m_spawnedGun = null;
        Instantiate(m_weapon.gameObject,
            m_playerTransform.position + m_playerTransform.forward,
            m_weapon.gameObject.transform.rotation);
    }

    public void DestroyGun()
    {
        Destroy(m_spawnedGun);
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponCell.OnWeaponChanged -= SpawnWeapon;
        m_equipmentInventory.WeaponCell.OnWeaponDropped -= DestroyGun;
        m_equipmentInventory.WeaponCell.OnWeaponDropped -= SpawnWeapon;
    }
}
