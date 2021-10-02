using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    void Start()
    {
        m_wearableItemsInventory.OnItemClicked += DropItem;
    }

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        wearableItemSlot.Clear();
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.OnItemClicked -= DropItem;
    }

}
