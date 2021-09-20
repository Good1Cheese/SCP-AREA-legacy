using UnityEngine;
using Zenject;

public class WearableItemsInteraction : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponSpawnerAndDestroyer m_weaponSpawnerAndDestroyer;

    void Start()
    {
        m_wearableItemsInventory.OnItemClicked += DropItem;
    }

    public void DropItem(WearableItemSlot wearableItemSlot)
    {
        wearableItemSlot.Clear();
        m_weaponSpawnerAndDestroyer.DespawnWeapon();
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.OnItemClicked -= DropItem;
    }

}
