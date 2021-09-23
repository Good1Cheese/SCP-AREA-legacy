using UnityEngine;
using Zenject;

public class InventoryDataSaving : DataSaving
{
    [Inject] readonly PickableItemsInventory m_playerInventory;
    public PickableItemHandler[] m_inventory;
    public string[] m_itemsName;

    public Transform PropsHandler { get; set; }

    void Start()
    {
        m_inventory = new PickableItemHandler[m_playerInventory.Inventory.Length];
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

    public override void Load()
    {
        m_playerInventory.CurrentItemIndex = 0;
        for (int i = 0; i < m_inventory.Length; i++)
        {
            m_playerInventory.Inventory[i] = m_inventory[i];
            if (m_inventory[i] != null) { m_playerInventory.CurrentItemIndex = i + 1; }
        }
        m_playerInventory.OnInventoryChanged?.Invoke();
    }

    public override void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        for (int i = 0; i < m_inventory.Length; i++)
        {
            if (string.IsNullOrEmpty(m_itemsName[i])) { return; }

            GameObject item = PropsHandler.Find(m_itemsName[i]).gameObject;
            item.GetComponent<ItemHandler>().Interact();
        }
    }
}
