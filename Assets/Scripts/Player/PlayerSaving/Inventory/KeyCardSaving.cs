using Zenject;

public class KeyCardSaving : WearableItemSaving
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => _wearableItemsInventory.KeyCardSlot;
}
