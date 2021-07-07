using System;
using UnityEngine;

[RequireComponent(typeof(EquipmentInventory))]
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int m_maxSlotsAmount;

    bool m_isUIActivated;
    Transform m_transform;
 
    public PickableItem_SO[] Inventory { get; set; }
    public Action OnInventoryChanged { get; set; }
    public Action<bool> OnInventoryButtonPressed { get; set; }
    public int CurrentItemIndex { get; set; }

    public Action<Vector2, InventorySlot> OnItemClicked { get; set; }

    void Start()
    {
        Inventory = new PickableItem_SO[m_maxSlotsAmount];
        m_transform = transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            m_isUIActivated = !m_isUIActivated;
            OnInventoryButtonPressed.Invoke(m_isUIActivated);
        }
    }

    public bool AddItem(PickableItem_SO item)
    {
        if (CurrentItemIndex >= m_maxSlotsAmount) { return false; }

        Inventory[CurrentItemIndex] = item;
        CurrentItemIndex++;

        OnInventoryChanged.Invoke();
        return true;
    }

    public void RemoveItem(Item_SO item)
    {
        int indexOfLastEnterance = 0;
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == item)
            {
                indexOfLastEnterance = i;
            }
        }

        Inventory[indexOfLastEnterance] = null;
        CurrentItemIndex = indexOfLastEnterance;

        OnInventoryChanged.Invoke();
    }

    public void SpawnItem(Item_SO item)
    {
        GameObject gameobjectOfItem = item.gameobject;
        Instantiate(gameobjectOfItem, m_transform.position, gameobjectOfItem.transform.rotation);
    }
}
