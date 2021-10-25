using System;
using UnityEngine;

[RequireComponent(typeof(ItemSaveableStateChanger))]
public abstract class ItemHandler : IInteractable
{
    private bool isInInventory;

    public bool IsInInventory
    {
        get => isInInventory;
        set
        {
            isInInventory = value;
            OnIsInventoryChanged?.Invoke(isInInventory);
        }
    }

    public Action<bool> OnIsInventoryChanged { get; set; }
    public GameObject GameObject { get; set; }

    void Start()
    {
        GameObject = gameObject;
    }

    public override void Interact()
    {
        IsInInventory = true;
        GameObject.SetActive(false);
        Equip();
    }

    public virtual void OnItemDropped() { }

    public abstract Item_SO GetItem();
    public abstract void Equip();
}
