using Zenject;

public class KeyCardSaving : WearableItemSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    public bool isInInventory;

    public override void Save()
    {
        if (m_wearableItemsInventory.KeyCardSlot.ItemHandler == null) { return; }

        itemName = m_wearableItemsInventory.KeyCardSlot.ItemHandler.name;
    }
}
