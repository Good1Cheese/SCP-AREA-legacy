using UnityEngine;

[RequireComponent(typeof(WeaponFire), typeof(WeaponReload))]
public class WeaponActivator : WearableItemActivator
{
    [SerializeField] private Transform _weaponParent;
    protected override WearableItemSlot WearableItemSlot => _wearableItemsInventory.WeaponSlot;

    private new void Start()
    {
        base.Start();
        _itemParent = _weaponParent;
    }

    public override void SetItemActiveState(bool itemActiveState)
    {
        base.SetItemActiveState(itemActiveState);
        _wearableItemsInventory.WeaponSlot.IsWeaponActived?.Invoke(itemActiveState);
    }

}
