public class DeviceActivator : WearableItemActivator
{
    void Awake()
    {
        m_wearableItemSlot = m_wearableItemsInventory.DeviceSlot;
    }
}
