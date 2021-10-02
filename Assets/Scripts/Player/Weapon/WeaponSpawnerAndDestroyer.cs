using UnityEngine;
using Zenject;

public class WeaponSpawnerAndDestroyer : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_equipmentInventory;
    [Inject] readonly Transform m_playerTransform;

    WeaponHandler m_weaponHandler;

    void Awake()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += HideGun;
    }

    public void SpawnWeapon(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;

        if (m_weaponHandler.PlayerWeapon == null)
        {
            m_weaponHandler.PlayerWeapon = Instantiate(weaponHandler.Weapon_SO.playerWeaponPrefab, transform);
        }

        m_weaponHandler.PlayerWeapon.SetActive(false);
    }

    public void DespawnWeapon()
    {
        m_weaponHandler.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        m_weaponHandler.gameObject.SetActive(true);
    }

    public void HideGun()
    {
        m_weaponHandler.PlayerWeapon.SetActive(false);
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SpawnWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= HideGun;
    }
}
