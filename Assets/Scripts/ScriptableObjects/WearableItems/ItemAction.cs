using UnityEngine;
using Zenject;

public abstract class ItemAction : MonoBehaviour
{
    [Inject] private readonly InventoryEnablerDisabler _inventoryAcviteStateSetter;

    protected abstract WearableSlot ItemSlot { get; }
    protected abstract WearableItemHandler WearableItemHandler { get; }

    protected void Start()
    {
        ItemSlot.OnItemToggled += SetActiveState;
        _inventoryAcviteStateSetter.OnInventoryEnabledDisabled += ChangeActiveStateOnInventoryEnabledDisabled;
        enabled = false;
    }

    private void ChangeActiveStateOnInventoryEnabledDisabled()
    {
        if (WearableItemHandler == null
            || !WearableItemHandler.GameObjectForPlayer.activeSelf
            || !WearableItemHandler.IsInInventory) { return; }

        SetActiveState(!enabled);
    }

    private void SetActiveState(bool activeState)
    {
        enabled = activeState;
    }

    protected void OnDestroy()
    {
        ItemSlot.OnItemToggled -= SetActiveState;
        _inventoryAcviteStateSetter.OnInventoryEnabledDisabled -= ChangeActiveStateOnInventoryEnabledDisabled;
    }
}