using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    [Inject] readonly WeaponSpawnerAndDestroyer m_weaponSpawnerAndDestroyer;

    void Start()
    {
        m_equipmentInventory.OnItemClicked += DropItem;
    }

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        wearableItemSlot.ClearSlot();
        m_weaponSpawnerAndDestroyer.SpawnWeapon();
    }

    void OnDestroy()
    {
        m_equipmentInventory.OnItemClicked -= DropItem;
    }

}
