using UnityEngine;
using Zenject;

public class InventorySaving : DataSaving
{
    [Inject] private readonly PickableItemsInventory _playerInventory;
    [Inject(Id = "PropsHandler")] private readonly Transform PropsHandler;

    public ItemHandler[] inventory;
    public string[] itemsName;

    private void Start()
    {
        inventory = new ItemHandler[_playerInventory.Inventory.Length];
        itemsName = new string[_playerInventory.Inventory.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = _playerInventory.Inventory[i];
            if (inventory[i] != null)
            {
                itemsName[i] = _playerInventory.Inventory[i].gameObject.name;
            }
        }
    }

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        for (int i = 0; i < inventory.Length; i++)
        {
            if (string.IsNullOrEmpty(itemsName[i])) { return; }

            GameObject item = PropsHandler.Find(itemsName[i]).gameObject;
            ItemHandler itemHandler = item.GetComponent<ItemHandler>();
            itemHandler.Interact();
        }
    }
}
