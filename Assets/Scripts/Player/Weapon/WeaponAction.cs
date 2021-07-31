using UnityEngine;
using Zenject;

public abstract class WeaponAction : MonoBehaviour
{
    [Inject] protected readonly EquipmentInventory m_equipmentInventory;
    [Inject] protected readonly PlayerInventory m_playerInventory;
    [Inject] protected WeaponActivator m_weaponActivator;

    protected Weapon_SO m_currentGun_SO;

    void Start()
    {
        m_equipmentInventory.WeaponCell.OnWeaponChanged += SetWeapon;
        m_equipmentInventory.WeaponCell.OnWeaponDropped += SetWeaponToZero;
        m_equipmentInventory.WeaponCell.OnWeaponActivated += SetActive;
        m_playerInventory.OnInventoryButtonPressed += SetActiveState;
        enabled = false;
    }

    protected void SetActiveState(bool isUIActive)
    {
        if (!m_weaponActivator.IsWeaponActive) { return; }
        enabled = !enabled;
    }

    protected void SetActive()
    {
        enabled = !enabled;
    }

    protected virtual void SetWeapon(Weapon_SO weapon)
    {
        m_currentGun_SO = weapon;
    }

    protected virtual void SetWeaponToZero()
    {
        m_currentGun_SO = null;
    }

    protected void OnDestroy()
    {
        m_equipmentInventory.WeaponCell.OnWeaponChanged -= SetWeapon;
        m_equipmentInventory.WeaponCell.OnWeaponDropped -= SetWeaponToZero;
        m_equipmentInventory.WeaponCell.OnWeaponActivated -= SetActive;
        m_playerInventory.OnInventoryButtonPressed -= SetActiveState;
    }
}