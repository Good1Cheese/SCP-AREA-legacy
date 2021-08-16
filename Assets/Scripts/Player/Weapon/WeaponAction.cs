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
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += SetWeaponToZero;
        m_equipmentInventory.WeaponSlot.OnWeaponActivatedOrDeactivated += SetActive;
        m_playerInventory.OnInventoryButtonPressed += SetActiveState; 
        enabled = false;
    }

    protected void SetActiveState()
    {
        if (!m_weaponActivator.IsWeaponActive) { return; }
        SetActive();
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
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= SetWeaponToZero;
        m_equipmentInventory.WeaponSlot.OnWeaponActivatedOrDeactivated -= SetActive;
        m_playerInventory.OnInventoryButtonPressed -= SetActiveState;
    }
}