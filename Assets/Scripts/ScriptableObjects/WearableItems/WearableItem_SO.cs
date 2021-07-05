public abstract class WearableItem_SO : Item_SO
{
    public new EquipmentInventory Inventory;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        Inventory = playerInstaller.EquipmentInventory;
    }
}

