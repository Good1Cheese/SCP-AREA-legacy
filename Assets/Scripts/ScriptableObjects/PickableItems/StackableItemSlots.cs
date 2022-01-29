public class StackableItemSlots
{
    private StackableItemSlot[] _slots;

    public StackableItemSlot[] Slots { get => _slots; }

    public StackableItemSlots(int stackSize)
    {
        _slots = new StackableItemSlot[stackSize];

        for (int i = 0; i < stackSize; i++)
        {
            Slots[i] = new StackableItemSlot();
        }
    }
}