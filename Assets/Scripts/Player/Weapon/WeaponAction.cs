using UnityEngine;
using Zenject;

public abstract class WeaponAction : MonoBehaviour
{
    [Inject] protected readonly WearableItemsInventory m_equipmentInventory;
    [Inject] protected readonly InventoryAcviteStateSetter m_inventoryAcviteStateSetter;
    [Inject] protected WeaponActivator m_weaponActivator;

    protected Weapon_SO m_currentGun_SO;

    void Start()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += SetWeaponToZero;
        m_equipmentInventory.WeaponSlot.IsWeaponActived += SetActive;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += SetActiveState; 
        enabled = false;
    }

    protected void SetActiveState()
    {
        if (!m_weaponActivator.IsWeaponActive) { return; }
        enabled = !enabled;
    }

    protected void SetActive(bool activeState)
    {
        enabled = activeState;
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
        m_equipmentInventory.WeaponSlot.IsWeaponActived -= SetActive;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= SetActiveState;
    }
}