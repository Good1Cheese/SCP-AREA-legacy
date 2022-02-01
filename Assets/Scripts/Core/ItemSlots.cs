public class ItemSlots<T>
{
    private ItemSlot<T>[] _slots;

    public ItemSlot<T>[] Slots { get => _slots; }

    public ItemSlots(int stackSize)
    {
        _slots = new ItemSlot<T>[stackSize];

        for (int i = 0; i < stackSize; i++)
        {
            Slots[i] = new ItemSlot<T>();
        }
    }
}