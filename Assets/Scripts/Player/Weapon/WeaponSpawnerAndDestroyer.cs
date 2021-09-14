using UnityEngine;
using Zenject;

public class WeaponSpawnerAndDestroyer : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_equipmentInventory;
    [Inject] readonly Transform m_playerTransform;

    public Weapon_SO Weapon { get; set; }
    GameObject m_spawnedGun;

    public GameObject CurrentGunGameObject { get => m_spawnedGun; }

    void Start()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += HideGun;
    }

    public void SpawnWeapon(Weapon_SO weapon_SO)
    {
        Weapon = weapon_SO;
        if (Weapon.playerWeapon == null)
        {
            Weapon.playerWeapon = Instantiate(weapon_SO.weaponForPlayerPrefab, transform);
        }
        m_spawnedGun = Weapon.playerWeapon;
        HideGun();
    }

    public void DespawnWeapon()
    {
        m_spawnedGun = null;
        Weapon.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        Weapon.gameObject.SetActive(true);
    }

    public void HideGun()
    {
        m_spawnedGun.SetActive(false);
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= HideGun;
    }
}
