using UnityEngine;
using Zenject;

public class PickableItemsInteraction : MonoBehaviour
{
    [Inject] public PlayerInventory PlayerInventory { get; }

    void Start()
    {
        PlayerInventory.OnItemRightClicked += DropItem;
        PlayerInventory.OnItemLeftClicked += UseItem;
    }

    public void UseItem(PickableItemSlot pickableItemSlot)
    {
        var item = pickableItemSlot.Item as PickableItem_SO;
        item.Use();
        PlayerInventory.RemoveItem(item);
    }

    public void DropItem(PickableItemSlot pickableItemSlot)
    {
        PlayerInventory.SpawnItem(pickableItemSlot);
    }

    void OnDestroy()
    {
        PlayerInventory.OnItemRightClicked -= DropItem;
        PlayerInventory.OnItemLeftClicked += UseItem;
    }
}
