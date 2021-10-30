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

    public override void LoadData()
    {
        ItemHandler.SetIsInventoty(isItemInInventory);

        if (!IsSaveable) { return; }

        base.LoadData();
    }
}