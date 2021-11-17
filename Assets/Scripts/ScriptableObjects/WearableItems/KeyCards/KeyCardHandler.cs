public class KeyCardHandler : WearableItemHandler
{
    public KeyCard_SO KeyCard_SO => (KeyCard_SO)_wearableIte_SO;

    public override void Equip()
    {
        _wearableItemsInventory.KeyCardSlot.SetItem(this);
    }
}
