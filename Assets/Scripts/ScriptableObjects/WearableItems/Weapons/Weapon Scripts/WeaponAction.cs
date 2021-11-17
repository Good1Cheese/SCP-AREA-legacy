using UnityEngine;
using Zenject;

public abstract class WeaponAction : MonoBehaviour
{
    [Inject] protected readonly WearableItemsInventory _wearableItemsInventory;
    [Inject] protected readonly InventoryEnablerDisabler _inventoryAcviteStateSetter;

    protected WeaponHandler _weaponHandler;

    protected void Start()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        _wearableItemsInventory.WeaponSlot.IsWeaponActived += SetActiveState;
        _inventoryAcviteStateSetter.OnInventoryEnabledDisabled += ChangeActiveStateOnInventoryEnabledDisabled;
        enabled = false;
    }

    private void ChangeActiveStateOnInventoryEnabledDisabled()
    {
        if (_weaponHandler == null
            || !_weaponHandler.GameObjectForPlayer.activeSelf
            || !_weaponHandler.IsInInventory) { return; }

        SetActiveState(!enabled);
    }

    private void SetActiveState(bool activeState)
    {
        enabled = activeState;
    }

    protected virtual void SetWeapon(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    protected void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        _wearableItemsInventory.WeaponSlot.IsWeaponActived -= SetActiveState;
        _inventoryAcviteStateSetter.OnInventoryEnabledDisabled -= ChangeActiveStateOnInventoryEnabledDisabled;
    }
}