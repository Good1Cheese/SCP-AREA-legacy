public abstract class PickableItem_SO : Item_SO
{
    protected PlayerInventory Inventory { get; set; }

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        Inventory = playerInstaller.PlayerInventory;
    }

    public override void Equip()
    {
        Inventory.AddItem(this);    
    }
}

