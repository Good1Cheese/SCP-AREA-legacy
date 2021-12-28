using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InventoryEnablerDisabler))]
public class PickableItemsInventory : MonoBehaviour
{
    [SerializeField, Range(0, 8)] private int _maxSlotsAmount;

    public ItemHandler[] Inventory { get; set; }
    public Action Changed { get; set; }
    public Action ItemRemoved { get; set; }
    public int CurrentItemIndex { get; set; }

    private void Awake()
    {
        Inventory = new ItemHandler[_maxSlotsAmount];
    }

    public void Add(ItemHandler item)
    {
        if (!HasEnoughSpaceForItem()) { return; }

        item.Equiped();
        Inventory[CurrentItemIndex] = item;
        CurrentItemIndex++;

        Changed?.Invoke();
    }


    public void Remove(int index)
    {
        Inventory[index].IsInInventory = false;
        Inventory[index] = null;

        bool isIndexItemLast = index > CurrentItemIndex;
        if (!isIndexItemLast)
        {
            for (int i = index + 1; Inventory[i] != null; i++)
            {
                Inventory[i - 1] = Inventory[i];
                Inventory[i].SetIsInventotyState(false);
                Inventory[i] = null;
            }
        }

        CurrentItemIndex = !isIndexItemLast ? CurrentItemIndex - 1 : index - 1;
        ItemRemoved?.Invoke();
        Changed?.Invoke();
    }

    public void Remove(ItemHandler itemHandler)
    {
        int index = Array.IndexOf(Inventory, itemHandler);

        if (index == -1)
        {
            Debug.LogError("Item is null!");
        }

        Remove(index);
    }

    public bool HasEnoughSpaceForItem() => CurrentItemIndex + 1 <= _maxSlotsAmount;

    public ItemHandler GetIem(Predicate<ItemHandler> condition)
    {
        return Inventory.TakeWhile(item => item != null).FirstOrDefault(item => condition.Invoke(item));
    }
}