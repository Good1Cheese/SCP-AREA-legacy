using UnityEngine;

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

    void Start()
    {
        GameObject = gameObject;
    }

    public virtual GameObject GameObject { get; set; }

    public void SetInInventoryState(bool value) => m_isInInventory = value;

    public override void Interact()
    {
        IsInInventory = true;
        GameObject.SetActive(false);
        Equip();
    }

    public virtual void OnInventoryStateChanged(bool isItemInInventory) { }

    public abstract Item_SO GetItem();
    public abstract void Equip();
}
