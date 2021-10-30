using Zenject;

public class KeyCardSaving : WearableItemSaving
{
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => m_wearableItemsInventory.KeyCardSlot;
}
