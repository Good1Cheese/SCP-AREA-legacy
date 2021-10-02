using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WearableItemsInventory), typeof(InventoryEnablerDisabler))]
public class PickableItemsInventory : MonoBehaviour
{
    [SerializeField, Range(0, 8)] int m_maxSlotsAmount;
    [SerializeField] Vector3 m_itemsOffsetForSpawn;

    [Inject] readonly Transform m_playerTransform;

    public PickableItemHandler[] Inventory { get; set; }
    public Action OnInventoryChanged { get; set; }
    public Action OnInventoryRemaded { get; set; }

    public Action<PickableItemSlot, int> OnItemLeftClicked { get; set; }
    public Action<PickableItemSlot, int> OnItemRightClicked { get; set; }

    public int CurrentItemIndex { get; set; }

    void Awake()
    {
        Inventory = new PickableItemHandler[m_maxSlotsAmount];
    }

    public void AddItem(PickableItemHandler item)
    {
        if (CurrentItemIndex >= m_maxSlotsAmount) { return; }

        Inventory[CurrentItemIndex] = item;
        CurrentItemIndex++;

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(int index)
    {
        Inventory[index].IsInInventory = false;
        Inventory[index] = null;

        bool isIndexItemLast = index > CurrentItemIndex;
        if (!isIndexItemLast)
        {
            for (int i = index + 1; Inventory[i] != null; i++)
            {
                Inventory[i - 1] = Inventory[i];
                Inventory[i].IsInInventory = false;
                Inventory[i] = null;
            }
        }

        CurrentItemIndex = !isIndexItemLast ? CurrentItemIndex - 1 : index - 1;
        OnInventoryChanged?.Invoke();
    }

    public void SpawnItem(PickableItemSlot slot)
    {
        GameObject gameobjectOfItem = slot.ItemHandler.gameObject;
        gameobjectOfItem.SetActive(true);
        gameobjectOfItem.transform.position = m_playerTransform.position + m_playerTransform.forward;
        RemoveItem(slot.SlotIndex);
    }
}
