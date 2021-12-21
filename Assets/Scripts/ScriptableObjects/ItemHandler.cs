using System;
using UnityEngine;

[RequireComponent(typeof(ItemSaveableStateChanger))]
public abstract class ItemHandler : IInteractable
{
    private bool _isInInventory;

    public bool IsInInventory
    {
        get => _isInInventory;
        set
        {
            _isInInventory = value;
            InventoryChanged?.Invoke(_isInInventory);
        }
    }

    public abstract Item_SO Item_SO { get; }
    public Action<bool> InventoryChanged { get; set; }
    public GameObject GameObject { get; set; }

    protected void Start()
    {
        GameObject = gameObject;
    }

    public void SetIsInventotyState(bool value)
    {
        _isInInventory = value;
    }

    public override void Interact()
    {
        Equip();
    }

    public void Equiped()
    {
        IsInInventory = true;
        GameObject.SetActive(false);
    }

    public abstract void Equip();

    public virtual void Dropped() { }
}