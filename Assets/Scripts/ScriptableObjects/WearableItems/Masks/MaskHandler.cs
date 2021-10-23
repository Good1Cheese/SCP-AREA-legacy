public class MaskHandler : WearableItemHandler
{
    public override void Equip()
    {
        m_wearableItemsInventory.MaskSlot.SetItem(this);
    }
}
