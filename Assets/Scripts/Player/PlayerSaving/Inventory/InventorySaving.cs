using UnityEngine;
using Zenject;

public class InventorySaving : DataSaving
{
    [Inject] readonly PickableItemsInventory m_playerInventory;
    [Inject(Id = "PropsHandler")] readonly Transform PropsHandler;

    public ItemHandler[] inventory;
    public string[] itemsName;

    void Start()
    {
        inventory = new ItemHandler[m_playerInventory.Inventory.Length];
        itemsName = new string[m_playerInventory.Inventory.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = m_playerInventory.Inventory[i];
            if (inventory[i] != null)
            {
                itemsName[i] = m_playerInventory.Inventory[i].gameObject.name;
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
