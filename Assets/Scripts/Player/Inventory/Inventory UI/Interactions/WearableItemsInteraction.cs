using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        wearableItemSlot.ItemHandler.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        wearableItemSlot.ItemHandler.gameObject.SetActive(true);

        wearableItemSlot.ItemHandler.IsInInventory = false;
        wearableItemSlot.Clear();
    }
}
