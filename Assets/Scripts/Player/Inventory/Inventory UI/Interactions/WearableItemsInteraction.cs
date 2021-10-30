using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        if (wearableItemSlot.IsItemActionGoing) { return; }

        wearableItemSlot.ItemHandler.GameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        wearableItemSlot.ItemHandler.GameObject.SetActive(true);

        wearableItemSlot.ItemHandler.IsInInventory = false;
        wearableItemSlot.Clear();
    }
}
