using UnityEngine;
using Zenject;

public abstract class WeaponAction : MonoBehaviour
{
    [Inject] protected readonly m_wearableItemsInventory m_wearableItemsInventory;
    [Inject] protected readonly InventoryEnablerDisabler m_inventoryAcviteStateSetter;

    protected WeaponHandler m_weaponHandler;

    protected void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived += SetActiveState;
        m_inventoryAcviteStateSetter.OnInventoryEnabledDisabled += ChangeActiveStateOnInventoryEnabledDisabled; 
        enabled = false;
    }

    void ChangeActiveStateOnInventoryEnabledDisabled()
    {
        if (m_weaponHandler == null
            || !m_weaponHandler.GameObjectForPlayer.activeSelf
            || !m_weaponHandler.IsInInventory) { return; }

        SetActiveState(!enabled);
    }

    void SetActiveState(bool activeState)
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
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived -= SetActiveState;
        m_inventoryAcviteStateSetter.OnInventoryEnabledDisabled -= ChangeActiveStateOnInventoryEnabledDisabled;
    }
}