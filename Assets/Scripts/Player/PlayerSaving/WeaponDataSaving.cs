using UnityEngine;
using Zenject;

public class WeaponDataSaving : DataSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public WeaponHandler savedWeaponHandler;
    public SilencerHandler silencer_SO;
    public bool isActive;
    public string weaponName;
    public int ammoCount;
    public int cartridgeСlipAmmo;

    public Transform PropsHandler { get; set; }

    public override void Save()
    {
        isActive = m_weaponActivator.IsWeaponActive;

        savedWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (savedWeaponHandler == null) { return; }

        silencer_SO = savedWeaponHandler.SilencerHandler;
        weaponName = m_wearableItemsInventory.WeaponSlot.ItemHandler.gameObject.name;
        ammoCount = savedWeaponHandler.AmmoCount;
        cartridgeСlipAmmo = savedWeaponHandler.ClipAmmo;
    }

    public override void Load()
    {
        ////if (m_equipmentInventory.WeaponSlot.Item != null && m_equipmentInventory.WeaponSlot.Item != savedWeapon) { m_equipmentInventory.WeaponSlot.Clear(); }

        //bool didPlayerTakeWeapon = savedWeaponHandler != null;

        //var weaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler;
        //if (weaponHandler != null)
        //{
        //    if (!didPlayerTakeWeapon)
        //    {
        //        weaponHandler.gameObject.SetActive(true);
        //    }

        //    if (m_weaponActivator.IsWeaponActive)
        //    {
        //        m_weaponActivator.SetWeaponActiveState(false);
        //    }

        //    if (weaponHandler != savedWeaponHandler) { m_wearableItemsInventory.WeaponSlot.Clear(); }
        //}

        //if (!didPlayerTakeWeapon) { return; }

        //m_wearableItemsInventory.WeaponSlot.SetItem(savedWeaponHandler);
        //LoadWeaponData();

        var currentWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler;

        if (currentWeaponHandler == null && savedWeaponHandler == null) { return; }
        if (currentWeaponHandler == savedWeaponHandler) { m_weaponActivator.SetWeaponActiveState(isActive); return; }

        if (isActive != m_weaponActivator.IsWeaponActive)
        {
            print(isActive);
            print(m_weaponActivator.IsWeaponActive);
            m_weaponActivator.SetWeaponActiveState(isActive);
        }

        if (currentWeaponHandler == null && savedWeaponHandler != null)
        {
            savedWeaponHandler.gameObject.SetActive(false);
            m_wearableItemsInventory.WeaponSlot.SetItem(savedWeaponHandler);

            return;
        }
        m_wearableItemsInventory.WeaponSlot.ClearSlot();
        currentWeaponHandler.gameObject.SetActive(true);

    }

    void LoadWeaponData()
    {
        savedWeaponHandler.IsInInventory = true;
        savedWeaponHandler.AmmoCount = ammoCount;
        savedWeaponHandler.ClipAmmo = cartridgeСlipAmmo;
        m_weaponActivator.SetWeaponActiveState(isActive);

        if (silencer_SO == null) { return; }
        savedWeaponHandler.SilencerHandler.Equip();

    }

    public override void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(weaponName)) { return; }
        GameObject weaponGameObject = PropsHandler.Find(weaponName).gameObject;

        weaponGameObject.GetComponent<ItemHandler>().Interact();
        LoadWeaponData();
    }
}
