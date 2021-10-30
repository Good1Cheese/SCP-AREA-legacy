using Zenject;

public class DeviceSaving : WearableItemSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => m_wearableItemsInventory.DeviceSlot;
}
