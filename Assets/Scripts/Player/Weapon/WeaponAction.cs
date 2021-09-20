using UnityEngine;
using Zenject;

public abstract class WeaponAction : MonoBehaviour
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] protected readonly WearableInventoryActivator m_inventoryAcviteStateSetter;
    [Inject] protected WeaponActivator m_weaponActivator;

    protected Weapon_SO m_weapon_SO;

    protected void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived += SetActive;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += SetActiveState; 
        enabled = false;
    }

    void SetActiveState()
    {
        if (!m_weaponActivator.IsWeaponActive) { return; }

        if (!m_weapon_SO.IsInInventory) { return; }

        SetActive(!enabled);
    }

    void SetActive(bool activeState)
    {
        enabled = activeState;
    }

    protected virtual void SetWeapon(Weapon_SO weapon_SO)
    {
        m_weapon_SO = weapon_SO;
    }

    protected void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived -= SetActive;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= SetActiveState;
    }
}