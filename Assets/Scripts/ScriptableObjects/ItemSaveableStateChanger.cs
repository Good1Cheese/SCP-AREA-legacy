using UnityEngine;

[RequireComponent(typeof(ItemSaving))]
public class ItemSaveableStateChanger : MonoBehaviour
{
    public ItemHandler ItemHandler { get; set; }
    public ItemSaving ItemDataHandler { get; set; }

    private void Awake()
    {
        ItemHandler = GetComponent<ItemHandler>();
        ItemDataHandler = GetComponent<ItemSaving>();
        ItemDataHandler.ItemHandler = ItemHandler;
    }

    private void Start()
    {
        ItemHandler.InventoryChanged += SetSaveableState;
    }

    public void SetSaveableState(bool isInventory)
    {
        ItemDataHandler.IsSaveable = isInventory;
    }

    private void OnDestroy()
    {
        ItemHandler.InventoryChanged -= SetSaveableState;
    }
}
