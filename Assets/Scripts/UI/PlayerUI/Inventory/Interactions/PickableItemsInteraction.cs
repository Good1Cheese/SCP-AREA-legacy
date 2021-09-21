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

    public void UseItem(PickableItemSlot pickableItemSlot)
    {
        var item = pickableItemSlot.Item as PickableItem_SO;
        item.Use();
        item.OnItemUsed();
    }

    public void DropItem(PickableItemSlot pickableItemSlot)
    {
        PickableItemsInventory.SpawnItem(pickableItemSlot);
    }

    void OnDestroy()
    {
        PickableItemsInventory.OnItemRightClicked -= DropItem;
        PickableItemsInventory.OnItemLeftClicked += UseItem;
    }
}
