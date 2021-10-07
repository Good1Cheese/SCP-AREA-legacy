using Zenject;

public class KeyCardSaving : WearableItemSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    public bool isInInventory;

    public override void Save()
    {
        itemHandler = m_wearableItemsInventory.KeyCardSlot.ItemHandler;

        if (itemHandler == null) { return; }

        itemName = m_wearableItemsInventory.KeyCardSlot.ItemHandler.name;
    }
}
