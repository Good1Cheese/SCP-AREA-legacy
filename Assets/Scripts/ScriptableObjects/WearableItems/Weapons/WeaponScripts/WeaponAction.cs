using UnityEngine;
using Zenject;

public abstract class WeaponAction : MonoBehaviour
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] protected readonly InventoryEnablerDisabler m_inventoryAcviteStateSetter;

    protected WeaponHandler m_weaponHandler;

    protected void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived += SetScriptActiveState;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += SetActiveState; 
        enabled = false;
    }

    void SetActiveState()
    {
        if (m_weaponHandler == null
            || !m_weaponHandler.WearableItemForPlayer.activeSelf
            || !m_weaponHandler.IsInInventory) { return; }

        SetScriptActiveState(!enabled);
    }

    void SetScriptActiveState(bool activeState)
    {
        enabled = activeState;
    }

    protected virtual void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;
    }

    protected void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived -= SetScriptActiveState;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= SetActiveState;
    }
}