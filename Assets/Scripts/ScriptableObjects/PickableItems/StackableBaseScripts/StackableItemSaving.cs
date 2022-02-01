public class StackableItemSaving : ItemSaving
{
    private StackableItemSlots _stackableItemSlots;

    private void Start()
    {
        var itemHandler = (StackableItemHandler)ItemHandler;
        _stackableItemSlots = new StackableItemSlots(itemHandler.StackSize);
    }

    public override void Save()
    {
        var b = (StackableItemHandler)ItemHandler;
        for (int i = 0; i < b.StackSlots.Slots.Length; i++)
        {
            _stackableItemSlots.Slots[i].StackableItemHandler = b.StackSlots.Slots[i].StackableItemHandler;
        }

        base.Save();
    }

    public override void Load()
    {
        var b = (StackableItemHandler)ItemHandler;
        for (int i = 0; i < b.StackSlots.Slots.Length; i++)
        {
            b.StackSlots.Slots[i].StackableItemHandler = _stackableItemSlots.Slots[i].StackableItemHandler;
        }

        base.Load();
    }
}