public class MaskHandler : WearableItemHandler
{
    public override void Equip()
    {
        _wearableItemsInventory.MaskSlot.SetItem(this);
    }
}
