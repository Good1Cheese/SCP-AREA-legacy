using UnityEngine;
using Zenject;

public abstract class PickableItemHandler : ItemHandler, IClickable
{
    [SerializeField] protected PickableItem_SO _pickableItem_SO;

    protected PickableItemsInventory _pickableItemsInventory;

    public virtual bool ShouldItemNotBeUsed => false;
    public override Item_SO Item_SO => _pickableItem_SO;

    [Inject]
    private void Inject(PickableItemsInventory pickableItemsInventory)
    {
        _pickableItemsInventory = pickableItemsInventory;
    }

    public override void Equip()
    {
        _pickableItemsInventory.Add(this);
    }

    public override void Clicked(int slotIndex)
    {
        if (!ShouldItemNotBeUsed) { return; }

        Clicked(slotIndex);
        _pickableItemsInventory.Remove(slotIndex);
    }
}