using UnityEngine;

[RequireComponent(typeof(WeaponFire), typeof(WeaponReload))]
public class WeaponActivator : WearableItemActivator
{
    void Awake()
    {
        m_wearableItemSlot = m_wearableItemsInventory.WeaponSlot;
    }

    public override void SetItemActiveState(bool itemActiveState)
    {
        base.SetItemActiveState(itemActiveState);
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived?.Invoke(itemActiveState);
    }

}
