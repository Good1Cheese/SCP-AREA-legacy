public class UtilityHandler : WearableItemHandler
{
    public override void Equip()
    {
        _wearableItemsInventory.UtilitySlot.SetItem(this);
    }
}
