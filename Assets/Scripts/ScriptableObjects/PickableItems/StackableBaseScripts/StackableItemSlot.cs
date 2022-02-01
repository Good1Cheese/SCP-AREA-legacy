public class StackableItemSlot
{
    public bool HasItem { get; set; }
    public StackableItemHandler StackableItemHandler { get; set; }

    public void Set(StackableItemHandler stackableItemHandler)
    {
        StackableItemHandler = stackableItemHandler;
        HasItem = true;
    }

    public StackableItemHandler GetItem()
    {
        StackableItemHandler itemHandler = StackableItemHandler;
        Clear();

        return itemHandler;
    }

    public void Clear()
    {
        StackableItemHandler = null;
        HasItem = false;
    }
}