using UnityEngine;
using Zenject;

public class PickableItemsInteraction : MonoBehaviour
{
    [Inject] public PickableItemsInventory PickableItemsInventory { get; }

    void Start()
    {
        PickableItemsInventory.OnItemRightClicked += DropItem;
        PickableItemsInventory.OnItemLeftClicked += UseItem;
    }

    public void UseItem(PickableItemSlot pickableItemSlot, int slotIndex)
    {
        var itemSlot = pickableItemSlot.ItemHandler as PickableItemHandler;
        itemSlot.OnItemClicked(slotIndex);
    }

    public void DropItem(PickableItemSlot pickableItemSlot, int slotIndex)
    {
        PickableItemsInventory.SpawnItem(pickableItemSlot);
    }   

    void OnDestroy()
    {
        PickableItemsInventory.OnItemRightClicked -= DropItem;
        PickableItemsInventory.OnItemLeftClicked -= UseItem;
    }
}
