public abstract class KeyCard_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.KeyCardSlot.SetItem(this);
    }

    public override bool HasPlayerThisItem()
    {
        return Inventory.KeyCardSlot.Item != null;
    }
}

    
