using UnityEngine;

[RequireComponent(typeof(ItemSaving))]
public class ItemSaveableStateChanger : MonoBehaviour
{
    public ItemHandler ItemHandler { get; set; }
    public ItemSaving ItemDataHandler { get; set; }

    void Awake()
    {
        ItemHandler = GetComponent<ItemHandler>();
        ItemDataHandler = GetComponent<ItemSaving>();
        ItemDataHandler.ItemHandler = ItemHandler;
    }

    void Start()
    {
        ItemHandler.OnIsInventoryChanged += SetSaveableState;
    }

    public void SetSaveableState(bool isInventory)
    {
        ItemDataHandler.IsSaveable = isInventory;
        if (isInventory)
        {
            ItemDataHandler.BecomeUnsaveable();
            return;
        }

        if (ItemDataHandler.IsSaveable) { return; }

        ItemDataHandler.BecomeSaveable();
    }

    void OnDestroy()
    {
        ItemHandler.OnIsInventoryChanged -= SetSaveableState;
    }
}
