using UnityEngine;
using Zenject;

public class PlayerSilencerSaving : WearableItemSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    WeaponHandler currentWeaponHandler;

    void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped += SetSilencerName;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped += SetSilencerNameToNull;
    }

    void SetSilencerNameToNull() => itemName = string.Empty;

    void SetSilencerName()
    {
        WeaponHandler currentWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;
        itemName = currentWeaponHandler.SilencerHandler.GameObject.name;
    }

    public override void Save()
    {
        currentWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (currentWeaponHandler == null) { return; }

        itemHandler = currentWeaponHandler.SilencerHandler;

        if (itemHandler == null) { return; }

        itemName = itemHandler.name;
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped -= SetSilencerName;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped -= SetSilencerNameToNull;
    }

}