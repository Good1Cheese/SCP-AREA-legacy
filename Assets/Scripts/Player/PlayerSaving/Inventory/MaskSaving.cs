using Zenject;

public class MaskSaving : WearableItemSaving
{
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => m_wearableItemsInventory.MaskSlot;
}
