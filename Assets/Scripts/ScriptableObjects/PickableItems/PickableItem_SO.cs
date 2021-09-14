public abstract class PickableItem_SO : Item_SO
{
    protected PickableItemsInventory Inventory { get; set; }

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        Inventory = playerInstaller.PlayerInventory;
    }

    public override void Equip()
    {
        Inventory.AddItem(this);    
    }

    public abstract void Use();
}

