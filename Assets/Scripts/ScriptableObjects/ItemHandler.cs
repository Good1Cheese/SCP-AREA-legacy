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
            OnIsInventoryChanged?.Invoke(_isInInventory);
        }
    }

    public Action<bool> OnIsInventoryChanged { get; set; }
    public GameObject GameObject { get; set; }

    protected void Start()
    {
        GameObject = gameObject;
    }

    public void SetIsInventoty(bool value)
    {
        _isInInventory = value;
    }

    public override void Interact()
    {
        IsInInventory = true;
        GameObject.SetActive(false);
        Equip();
    }

    public virtual void OnItemDropped() { }
    public abstract Ite_SO GetItem();

    public abstract void Equip();
}
