using Zenject;

public class DeviceSaving : WearableItemSaving
{
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => m_wearableItemsInventory.DeviceSlot;
}
