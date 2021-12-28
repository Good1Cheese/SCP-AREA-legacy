using System;
using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    [Inject] private readonly ItemActionCreator _itemActionCreator;

    public Action ItemRemoved { get; set; }

    public void DropItem(WearableSlot wearableItemSlot)
    {
        if (_itemActionCreator.IsGoing) { return; }

        ItemRemoved?.Invoke();
        wearableItemSlot.ItemHandler.GameObject.transform.position = _playerTransform.position + _playerTransform.forward;
        wearableItemSlot.ItemHandler.GameObject.SetActive(true);

        wearableItemSlot.ItemHandler.IsInInventory = false;
        wearableItemSlot.Clear();
    }
}