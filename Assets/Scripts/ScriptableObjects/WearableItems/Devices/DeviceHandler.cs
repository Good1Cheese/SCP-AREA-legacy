using UnityEngine;
using Zenject;

public class DeviceHandler : WearableItemHandler
{
    [SerializeField] Device_SO m_device_SO;

    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

    void Awake()
    {
        WearableItemForPlayer = Instantiate(m_device_SO.flashLightPrefab);
        WearableItemForPlayer.SetActive(false);
    }

    public override void Equip()
    {
        m_wearableItemsInventory.DeviceSlot.SetItem(this);
    }

    public override Item_SO GetItem() => m_device_SO;
}
