public abstract class PickableItem_SO : Item_SO
{
    protected PickableItemsInventory PickableItemsInventory { get; set; }

    public virtual void GetDependencies(PlayerInstaller playerInstaller)
    {
        PickableItemsInventory = playerInstaller.PickableItemsInventory;
    }

    public abstract void Use();

    public virtual bool ShouldItemNotUsed() => false;
}

