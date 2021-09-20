using UnityEngine;
using Zenject;

public class WeaponSpawnerAndDestroyer : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_equipmentInventory;
    [Inject] readonly Transform m_playerTransform;

    Weapon_SO m_weapon_SO;

    void Awake()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += HideGun;
    }

    public void SpawnWeapon(Weapon_SO weapon_SO)
    {
        m_weapon_SO = weapon_SO;

        if (m_weapon_SO.playerWeapon == null)
        {
            m_weapon_SO.playerWeapon = Instantiate(weapon_SO.playerWeaponPrefab, transform);
        }

        m_weapon_SO.playerWeapon.SetActive(false);
    }

    public void DespawnWeapon()
    {
        m_weapon_SO.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        m_weapon_SO.gameObject.SetActive(true);
    }

    public void HideGun()
    {
        m_weapon_SO.playerWeapon.SetActive(false);
    }


    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= HideGun;
    }
}
