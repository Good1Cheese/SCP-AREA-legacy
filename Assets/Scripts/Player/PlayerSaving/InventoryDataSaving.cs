using UnityEngine;
using Zenject;

public class InventoryDataSaving : DataHandler
{
    [Inject] readonly PickableItemsInventory m_playerInventory;
    public PickableItem_SO[] m_inventory;
    public string[] m_itemsName;

    public Transform PropsHandler { get; set; }

    void Start()
    {
        m_inventory = new PickableItem_SO[m_playerInventory.Inventory.Length];
        m_itemsName = new string[m_playerInventory.Inventory.Length];
    }

    public override void SaveData()
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

    public override void LoadData()
    {
        m_playerInventory.CurrentItemIndex = 0;
        for (int i = 0; i < m_inventory.Length; i++)
        {
            m_playerInventory.Inventory[i] = m_inventory[i];
            if (m_inventory[i] != null) { DisableItem(m_playerInventory.Inventory[i]); m_playerInventory.CurrentItemIndex = i + 1; }
        }
        m_playerInventory.OnInventoryChanged.Invoke();
    }

    void DisableItem(Item_SO item_SO)
    {
        item_SO.gameObject.SetActive(item_SO.IsInInventory);
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
