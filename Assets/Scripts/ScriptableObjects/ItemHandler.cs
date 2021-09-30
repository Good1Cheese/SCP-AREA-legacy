using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemDataController))]
public abstract class ItemHandler : IInteractable
{
    bool m_isInInventory;

    public bool IsInInventory 
    {
        get => m_isInInventory;
        set
        {
            m_isInInventory = value;
            OnInventoryStateChanged(m_isInInventory);
        }
    }

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

    public virtual void OnInventoryStateChanged(bool isItemInInventory)
    {

    }

    public abstract Item_SO GetItem();  

    public abstract void Equip();

}
