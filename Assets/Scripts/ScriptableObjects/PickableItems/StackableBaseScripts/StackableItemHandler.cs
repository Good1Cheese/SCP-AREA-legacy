using System;
using UnityEngine;

[RequireComponent(typeof(StackableItemSaving))]
public abstract class StackableItemHandler : PickableItemHandler
{
    [SerializeField] private int _stackSize;

    private StackableItemSlots _stackableItemSlots;
    private StackableItemSlot _freeSlot;

    public StackableItemSlots StackSlots => _stackableItemSlots;
    public int StackSize => _stackSize;

    protected new void Start()
    {   
        base.Start();

        _stackableItemSlots = new StackableItemSlots(StackSize);
        _stackableItemSlots.Slots[0].Set(this);
    }

    public override void Equip()
    {
        var lastItemHandler = (StackableItemHandler)Array.FindLast(_pickableItemsInventory.Inventory,
                                   itemHandler => itemHandler as StackableItemHandler);

        if (lastItemHandler == null
            || (_freeSlot = Array.Find(lastItemHandler._stackableItemSlots.Slots, slot => !slot.HasItem)) == null) 
        {
            base.Equip();
            return; 
        }

        _freeSlot.Set(this);
        Equiped();
    }
}