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
        pickableItemSlot.ItemHandler.GameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        pickableItemSlot.ItemHandler.GameObject.SetActive(true);

        pickableItemSlot.Clear();
        PickableItemsInventory.RemoveItem(pickableItemSlot.SlotIndex);
    }
}
