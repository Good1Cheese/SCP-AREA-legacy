using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemDataController))]
public abstract class ItemHandler : IInteractable
{
    public bool IsInInventory { get; set; }
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

    public abstract Item_SO GetItem();

    public abstract void Equip();

    protected virtual void OnDestroy()
    {
        IsInInventory = false;
    }
}

public abstract class WearableItemHandler : ItemHandler
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
}
