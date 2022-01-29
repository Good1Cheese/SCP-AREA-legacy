using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSaveableStateChanger))]
public abstract class ItemHandler : Interactable, IClickable
{
    [Inject(Id = "Player")] private readonly Transform _playerTransform;

    private bool _isInInventory;

    public bool IsInInventory
    {
        get => _isInInventory;
        set
        {
            _isInInventory = value;
            IsInventoryChanged?.Invoke(_isInInventory);
        }
    }

    public Action<bool> IsInventoryChanged { get; set; }
    public GameObject GameObject { get; set; }
    public abstract Item_SO Item_SO { get; }

    protected void Start()
    {
        GameObject = gameObject;
    }

    public void SetIsInventotyState(bool value)
    {
        _isInInventory = value;
    }

    public void Equiped()
    {
        IsInInventory = true;
        GameObject.SetActive(false);
    }

    public override void Interact()
    {
        Equip();
    }

    public virtual void Clicked(int slotIndex)
    {
        Use();
    }

    public virtual void Use() 
    {
        print("Used in " + this);
    }

    public virtual void Dropped() 
    {
        GameObject.SetActive(true);
        GameObject.transform.position = _playerTransform.position + _playerTransform.forward;
    }

    public abstract void Equip();
}