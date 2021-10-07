using System;

public class KeyCardSlot : WearableItemSlot
{
    public new void SetItem(ItemHandler item)
    {
        if (ItemHandler != null)
        {
            Clear();
        }

        base.SetItem(item);
    }

    public override void OnItemSet()
    {
        base.OnItemSet();
    }

    public override void OnItemDeleted()
    {
        base.OnItemDeleted();
    }
}