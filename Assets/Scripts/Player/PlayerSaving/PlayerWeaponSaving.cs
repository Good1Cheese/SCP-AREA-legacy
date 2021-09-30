using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerWeaponSaverLoader))]
public class PlayerWeaponSaving : DataSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public Transform PropsHandler { get; set; }
    public PlayerWeaponSaverLoader WeaponData { get; set; }
    public WeaponHandler CurrentWeaponHandler { get; set ; }

    void Start()
    {
        WeaponData = GetComponent<PlayerWeaponSaverLoader>();
    }

    public override void Save()
    {
        WeaponData.Save();
    }

    public override void Load()
    {
        CurrentWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (CurrentWeaponHandler == null && WeaponData.savedWeaponHandler == null) { return; }

        if (CurrentWeaponHandler == WeaponData.savedWeaponHandler)
        {
            WeaponData.LoadWeaponData();
            return;
        }

        if (WeaponData.isActive != m_weaponActivator.IsWeaponActive)
        {
            m_weaponActivator.SetWeaponActiveState(WeaponData.isActive);
        }

        if (CurrentWeaponHandler == null && WeaponData.savedWeaponHandler != null)
        {
            m_wearableItemsInventory.WeaponSlot.SetItem(WeaponData.savedWeaponHandler);
            WeaponData.LoadWeaponData();

            return;
        }

        m_wearableItemsInventory.WeaponSlot.ClearWearableSlot();
    }

    public override void LoadFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, WeaponData);

        if (string.IsNullOrEmpty(WeaponData.weaponName)) { return; }

        GameObject weaponGameObject = PropsHandler.Find(WeaponData.weaponName).gameObject;
        WeaponHandler weaponHandler = weaponGameObject.GetComponent<ItemHandler>() as WeaponHandler;

        WeaponData.savedWeaponHandler = weaponHandler;
        weaponHandler.Interact();

        WeaponData.LoadWeaponData();
    }

    public override string ToJson() => JsonUtility.ToJson(WeaponData);
}
