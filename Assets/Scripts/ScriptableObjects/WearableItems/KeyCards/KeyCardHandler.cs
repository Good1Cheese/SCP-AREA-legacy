public class KeyCardHandler : WearableItemHandler
{
    public KeyCard_SO KeyCard_SO { get => (KeyCard_SO)m_wearableItem_SO; }

    public override void Equip()
    {
        m_wearableItemsInventory.KeyCardSlot.SetItem(this);
    }
}
