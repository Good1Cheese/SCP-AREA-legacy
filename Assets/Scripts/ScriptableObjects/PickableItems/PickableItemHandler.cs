using UnityEngine;
using Zenject;

public abstract class PickableItemHandler : ItemHandler, IClickable
{
    [SerializeField] protected PickableIte_SO _pickableIte_SO;

    [Inject] protected readonly PickableItemsInventory _pickableItemsInventory;

    public virtual void Use() { }

    public virtual void Clicked(int slotIndex)
    {
        if (!ShouldItemNotBeUsed) { return; }
        Use();
        _pickableItemsInventory.RemoveItem(slotIndex);
    }


    public override void Equip()
    {
        _pickableItemsInventory.AddItem(this);
    }

    public virtual bool ShouldItemNotBeUsed => false;
    public override Ite_SO GetItem()
    {
        return _pickableIte_SO;
    }
}
