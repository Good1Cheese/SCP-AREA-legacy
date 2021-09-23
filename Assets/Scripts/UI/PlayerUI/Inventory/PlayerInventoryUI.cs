using UnityEngine;
using Zenject;

public class PlayerInventoryUI : MonoBehaviour
{
    [Inject] readonly PickableItemsInventory m_playerInventory;
    GameObject m_gameObject;

    public PickableItemSlot[] InventoryCells { get; set; }

    void Awake()
    {
        m_gameObject = gameObject;
        m_playerInventory.OnInventoryChanged += Renew;
        m_playerInventory.OnInventoryRemaded += Renew;
    }

    void Start()
    {
        InventoryCells = transform.GetComponentsInChildren<PickableItemSlot>();

        for (int i = 0; i < InventoryCells.Length; i++)
        {
            InventoryCells[i].SlotIndex = i;
            InventoryCells[i].gameObject.SetActive(false);
        }

        m_gameObject.SetActive(false);
    }

    void Renew()
    {
        int inventoryLength = m_playerInventory.Inventory.Length;
        for (int i = 0; i < inventoryLength; i++)
        {
            PickableItemHandler item = m_playerInventory.Inventory[i];
            if (item != null)
            {
                InventoryCells[i].SetItem(item);
                continue;
            }

            if (InventoryCells[i].ItemHandler != null)
            {
                InventoryCells[i].Clear();
            }
        }
    }

    public void ActivateOrClose()
    {
        m_gameObject.SetActive(!m_gameObject.activeSelf);
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryChanged -= Renew;
        m_playerInventory.OnInventoryRemaded -= Renew;
    }

}
