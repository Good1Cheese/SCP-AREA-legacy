using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemDataSaving))]
public class ItemDataController : MonoBehaviour
{
    [Inject] readonly PickableItemsInventory m_playerInventory;

    public ItemHandler ItemHandler { get; set; }
    public ItemDataSaving ItemDataHandler { get; set; }

    void Awake()
    {
        ItemHandler = GetComponent<ItemHandler>();
        ItemDataHandler = GetComponent<ItemDataSaving>();
    }

    void Start()
    {
        m_playerInventory.OnInventoryChanged += SetSavableState;
    }

    void SetSavableState()
    {
        if (ItemHandler.IsInInventory)
        {
            ItemDataHandler.BecomeUnsaveable();
            return;
        }
        if (ItemDataHandler.IsSubscribed) { return; }
        ItemDataHandler.BecomeSaveable();
    }

    public void SetSavableState(bool isSaveable)
    {
        if (isSaveable)
        {
            ItemDataHandler.BecomeSaveable();
            return;
        }
        ItemDataHandler.BecomeUnsaveable();
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryChanged -= SetSavableState;
    }
}
