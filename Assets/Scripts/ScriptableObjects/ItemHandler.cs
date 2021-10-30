using System;
using UnityEngine;

[RequireComponent(typeof(ItemSaveableStateChanger))]
public abstract class ItemHandler : IInteractable
{
    private bool m_isInInventory;

    public bool IsInInventory
    {
        get => m_isInInventory;
        set
        {
            m_isInInventory = value;
            OnIsInventoryChanged?.Invoke(m_isInInventory);
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
        m_isInInventory = value;
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
