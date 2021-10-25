using UnityEngine;
using Zenject;

public class PickableItemsInteraction : MonoBehaviour
{
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    [Inject] public PickableItemsInventory PickableItemsInventory { get; }

    public void UseItem(PickableItemSlot pickableItemSlot)
    {
        var itemSlot = pickableItemSlot.ItemHandler as IClickable;
        itemSlot.Clicked(pickableItemSlot.SlotIndex);
    }

    public void DropItem(PickableItemSlot pickableItemSlot)
    {
        GameObject gameobjectOfItem = pickableItemSlot.ItemHandler.gameObject;
        gameobjectOfItem.SetActive(true);
        gameobjectOfItem.transform.position = m_playerTransform.position + m_playerTransform.forward;

        PickableItemsInventory.RemoveItem(pickableItemSlot.SlotIndex);
    }
}
