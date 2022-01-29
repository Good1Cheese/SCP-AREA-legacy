public class ItemSaving : GameObjectSaving
{
    public bool isItemInInventory;

    public ItemHandler ItemHandler { get; set; }
    public bool IsSaveable { get; set; } = true;

    public override void Save()
    {
        isItemInInventory = ItemHandler.IsInInventory;
        base.Save();
    }

    public override void Load()
    {
        ItemHandler.SetIsInventotyState(isItemInInventory);

        if (!IsSaveable) { return; }

        base.Load();
    }
}