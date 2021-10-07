using UnityEngine;
using Zenject;

public class DoorInteractable : IInteractable
{
    [SerializeField] int m_levelForOpen;

    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    public override void Interact()
    {
        var keycardHandler = m_wearableItemsInventory.KeyCardSlot.ItemHandler as KeyCardHandler;

        if (keycardHandler == null) { return; }

        print(keycardHandler.KeyCard_SO.GetKeyCardLevel());
        if (keycardHandler.KeyCard_SO.GetKeyCardLevel() >= m_levelForOpen)
        {
            print("dsa");
        }
    }
}

