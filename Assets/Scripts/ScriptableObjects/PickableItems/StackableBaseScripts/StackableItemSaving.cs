public class StackableItemSaving : ItemSaving
{
    private ItemSlots<StackableItemHandler> _stackableItemSlots;

    private void Start()
    {
        var itemHandler = (StackableItemHandler)ItemHandler;
        _stackableItemSlots = new ItemSlots<StackableItemHandler>(itemHandler.StackSize);
    }

    public override void Save()
    {
        var b = (StackableItemHandler)ItemHandler;
        for (int i = 0; i < b.StackSlots.Slots.Length; i++)
        {
            _stackableItemSlots.Slots[i].Item = b.StackSlots.Slots[i].Item;
        }

        base.Save();
    }

    public override void Load()
    {
        var b = (StackableItemHandler)ItemHandler;
        for (int i = 0; i < b.StackSlots.Slots.Length; i++)
        {
            b.StackSlots.Slots[i].Item = _stackableItemSlots.Slots[i].Item;
        }

        base.Load();
    }
}