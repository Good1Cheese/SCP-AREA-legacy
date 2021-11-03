using UnityEngine;

[RequireComponent(typeof(WeaponFire), typeof(WeaponReload))]
public class WeaponActivator : WearableItemActivator
{
    [SerializeField] Transform m_weaponParent;
    protected override WearableItemSlot WearableItemSlot => m_wearableItemsInventory.WeaponSlot;

    new void Start()
    {
        base.Start();
        m_itemParent = m_weaponParent;
    }

    public override void SetItemActiveState(bool itemActiveState)
    {
        base.SetItemActiveState(itemActiveState);
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived?.Invoke(itemActiveState);
    }

}
