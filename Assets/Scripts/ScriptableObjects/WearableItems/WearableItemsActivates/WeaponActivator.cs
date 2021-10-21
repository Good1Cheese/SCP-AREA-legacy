using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponFire), typeof(WeaponReload))]
public class WeaponActivator : WearableItemActivator
{
    [Inject] readonly InventoryEnablerDisabler m_inventoryAcviteStateSetter;

    void Awake()
    {
        m_wearableItemSlot = m_wearableItemsInventory.WeaponSlot;

        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += DeactivateWeapon;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += SetActiveState;
    }

    public override void SetItemActiveState(bool itemActiveState)
    {
        base.SetItemActiveState(itemActiveState);
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived?.Invoke(itemActiveState);
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    void DeactivateWeapon()
    {
        m_wearableItemHandler.WearableItemForPlayer.SetActive(false);
        m_wearableItemHandler = null;
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped -= DeactivateWeapon;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= SetActiveState;
    }
}
