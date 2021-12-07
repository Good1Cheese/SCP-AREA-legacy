using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject(Id = "Player")] private readonly Transform _playerTransform;

    public void DropItem(WearableSlot wearableItemSlot)
    {
        if (wearableItemSlot.IsItemActionGoing) { return; }

        wearableItemSlot.ItemHandler.GameObject.transform.position = _playerTransform.position + _playerTransform.forward;
        wearableItemSlot.ItemHandler.GameObject.SetActive(true);

        wearableItemSlot.ItemHandler.IsInInventory = false;
        wearableItemSlot.Clear();
    }
}
