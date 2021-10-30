using UnityEngine;
using Zenject;

public class InventorySaving : DataSaving
{
    [Inject] readonly PickableItemsInventory m_playerInventory;
    [Inject(Id = "PropsHandler")] readonly Transform PropsHandler;

    public ItemHandler[] m_inventory;
    public string[] m_itemsName;

    void Start()
    {
        m_inventory = new ItemHandler[m_playerInventory.Inventory.Length];
        m_itemsName = new string[m_playerInventory.Inventory.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < m_inventory.Length; i++)
        {
            m_inventory[i] = m_playerInventory.Inventory[i];
            if (m_inventory[i] != null)
            {
                m_itemsName[i] = m_playerInventory.Inventory[i].gameObject.name;
            }
        }
    }

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        for (int i = 0; i < m_inventory.Length; i++)
        {
            if (string.IsNullOrEmpty(m_itemsName[i])) { return; }

            GameObject item = PropsHandler.Find(m_itemsName[i]).gameObject;
            ItemHandler itemHandler = item.GetComponent<ItemHandler>();
            itemHandler.Interact();
        }
    }
}
