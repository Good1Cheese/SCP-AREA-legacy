public abstract class KeyCard_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.KeyCardSlot.SetItem(this);
    }
}