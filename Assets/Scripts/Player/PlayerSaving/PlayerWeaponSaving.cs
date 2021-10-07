using UnityEngine;
using Zenject;

public class PlayerWeaponSaving : WearableItemSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public bool isActive;
    public int ammoCount;
    public int clipAmmo;

    WeaponHandler WeaponHandler { get => itemHandler as WeaponHandler; }

    public override void Save()
    {
        isActive = m_weaponActivator.IsWeaponActive;

        itemHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler;

        if (itemHandler == null) { return; }

        ammoCount = WeaponHandler.AmmoCount;
        clipAmmo = WeaponHandler.ClipAmmo;

        itemName = itemHandler.name;
    }

    public override void Load(string json)
    {
        base.Load(json);

        if (itemHandler == null) { return; }

        LoadWeaponData();
    }

    public void LoadWeaponData()
    {
        WeaponHandler.AmmoCount = ammoCount;
        WeaponHandler.ClipAmmo = clipAmmo;
        m_weaponActivator.SetWeaponActiveState(isActive);
    }
}
