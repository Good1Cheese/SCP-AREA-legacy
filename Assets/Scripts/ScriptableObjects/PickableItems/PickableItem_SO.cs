public abstract class PickableItem_SO : Item_SO
{
    protected PickableItemsInventory PickableItemsInventory { get; set; }
    protected PlayerInstaller PlayerInstaller { get; set; }
    protected GameControllerInstaller GameControllerInstaller { get; set; }

    public virtual void GetDependencies(PlayerInstaller playerInstaller, GameControllerInstaller gameControllerInstaller)
    {
        PickableItemsInventory = gameControllerInstaller.PickableItemsInventory;
        PlayerInstaller = PlayerInstaller;
        PickableItemsInventory = gameControllerInstaller.PickableItemsInventory;
    }

    public abstract void Use();

    public virtual bool ShouldItemNotUsed() => false;
}

