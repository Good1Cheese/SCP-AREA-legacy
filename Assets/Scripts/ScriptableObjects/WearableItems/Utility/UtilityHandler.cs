public class UtilityHandler : WearableItemHandler
{
    public override void Equip()
    {
        m_wearableItemsInventory.UtilitySlot.SetItem(this);
    }
}
