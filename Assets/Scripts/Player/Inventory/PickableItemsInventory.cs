using System;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WearableItemsInventory), typeof(InventoryEnablerDisabler))]
public class PickableItemsInventory : MonoBehaviour
{
    [SerializeField, Range(0, 8)] private int _maxSlotsAmount;
    [SerializeField] private Vector3 _itemsOffsetForSpawn;

    [Inject(Id = "Player")] private readonly Transform _playerTransform;

    public ItemHandler[] Inventory { get; set; }
    public Action OnInventoryChanged { get; set; }
    public Action OnInventoryRemaded { get; set; }

    public int CurrentItemIndex { get; set; }

    private void Awake()
    {
        Inventory = new ItemHandler[_maxSlotsAmount];
    }

    public void AddItem(ItemHandler item)
    {
        if (CurrentItemIndex >= _maxSlotsAmount) { return; }

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
                Inventory[i].SetIsInventoty(false);
                Inventory[i] = null;
            }
        }

        CurrentItemIndex = !isIndexItemLast ? CurrentItemIndex - 1 : index - 1;
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemHandler itemHandler)
    {
        int index = Array.IndexOf(Inventory, itemHandler);

        if (index == -1)
        {
            Debug.LogError("Item is null!");
        }

        RemoveItem(index);
    }

    public ItemHandler GetIem(Predicate<ItemHandler> condition)
    {
        return Inventory.TakeWhile(item => item != null).LastOrDefault(item => condition.Invoke(item));
    }
}