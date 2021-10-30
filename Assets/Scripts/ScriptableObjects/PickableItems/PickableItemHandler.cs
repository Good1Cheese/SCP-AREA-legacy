using UnityEngine;
using Zenject;

public abstract class PickableItemHandler : ItemHandler, IClickable
{
    [SerializeField] protected PickableItem_SO m_pickableItem_SO;

    [Inject] protected readonly PickableItemsInventory m_pickableItemsInventory;

    public virtual void Use() { }
    
    public virtual void Clicked(int slotIndex)
    {
        if (!ShouldItemNotBeUsed) { return; }
        Use();
        m_pickableItemsInventory.RemoveItem(slotIndex);
    }


    public override void Equip()
    {
        m_pickableItemsInventory.AddItem(this);
    }

    public virtual bool ShouldItemNotBeUsed => false;
    public override Item_SO GetItem() => m_pickableItem_SO;
}
