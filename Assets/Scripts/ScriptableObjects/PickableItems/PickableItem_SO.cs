using System;

public abstract class PickableItem_SO : Item_SO
{
    protected PickableItemsInventory PickableItemsInventory { get; set; }

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        PickableItemsInventory = playerInstaller.PlayerInventory;
    }

    public override void Equip()
    {
        PickableItemsInventory.AddItem(this);    
    }

    public abstract void Use();

    public virtual void OnItemUsed()
    {
        PickableItemsInventory.RemoveItem(this);
    }
}

