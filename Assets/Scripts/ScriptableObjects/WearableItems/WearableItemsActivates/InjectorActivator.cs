public class InjectorActivator : WearableItemActivator
{
    protected override WearableItemSlot WearableItemSlot => _wearableItemsInventory.InjectorSlot;

    private new void Start()
    {
        base.Start();
        _wearableItemsInventory.InjectorSlot.OnSlotUsed += ActivateItem;
    }

    private void ActivateItem()
    {
        SetItemActiveState(true);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _wearableItemsInventory.InjectorSlot.OnSlotUsed -= ActivateItem;
    }
}
