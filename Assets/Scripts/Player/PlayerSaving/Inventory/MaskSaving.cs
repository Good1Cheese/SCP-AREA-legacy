using Zenject;

public class MaskSaving : WearableItemSaving
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => _wearableItemsInventory.MaskSlot;
}
