using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_equipmentInventory;
    [Inject] readonly WeaponSpawnerAndDestroyer m_weaponSpawnerAndDestroyer;

    void Start()
    {
        m_equipmentInventory.OnItemClicked += DropItem;
    }

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        wearableItemSlot.Clear();
        m_weaponSpawnerAndDestroyer.DespawnWeapon();
    }

    void OnDestroy()
    {
        m_equipmentInventory.OnItemClicked -= DropItem;
    }

}
