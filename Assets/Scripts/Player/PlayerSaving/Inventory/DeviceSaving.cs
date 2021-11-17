using Zenject;

public class DeviceSaving : WearableItemSaving
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => _wearableItemsInventory.UtilitySlot;
}
