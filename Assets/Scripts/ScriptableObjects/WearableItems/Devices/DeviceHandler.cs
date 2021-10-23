public class DeviceHandler : WearableItemHandler
{
    public override void Equip()
    {
        m_wearableItemsInventory.DeviceSlot.SetItem(this);
    }
}
