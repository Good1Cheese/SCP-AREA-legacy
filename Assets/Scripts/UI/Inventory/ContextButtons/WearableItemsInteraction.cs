using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;

    void Start()
    {
        m_equipmentInventory.OnItemClicked += DropItem;
    }

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        wearableItemSlot.ClearSlot();
    }

    void OnDestroy()
    {
        m_equipmentInventory.OnItemClicked -= DropItem;
    }

}
