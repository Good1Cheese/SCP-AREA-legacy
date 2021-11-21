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

    public abstract Ite_SO Item { get; }
    public Action<bool> OnIsInventoryChanged { get; set; }
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
        IsInInventory = true;
        GameObject.SetActive(false);
        Equip();
    }

    public abstract void Equip();

    public virtual void OnItemDropped() { }
}
