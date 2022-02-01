public class ItemSlot<T>
{
    public bool HasItem { get; set; }
    public T Item { get; set; }

    public T GetItem()
    {
        T itemHandler = Item;
        Clear();

        return itemHandler;
    }

    public void Set(T item)
    {
        Item = item;
        HasItem = true;
    }

    public void Clear()
    {
        Item = default;
        HasItem = false;
    }
}