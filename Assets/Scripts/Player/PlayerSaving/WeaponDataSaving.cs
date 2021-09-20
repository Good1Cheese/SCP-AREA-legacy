using UnityEngine;
using Zenject;

public class WeaponDataSaving : DataSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public Weapon_SO savedWeapon;
    public Silencer_SO silencer_SO;
    public bool isActive;
    public string weaponName;
    public int ammoCount;
    public int cartridgeСlipAmmo;

    public Transform PropsHandler { get; set; }

    public override void Save()
    {
        isActive = m_weaponActivator.IsWeaponActive;

        savedWeapon = m_wearableItemsInventory.WeaponSlot.Item as Weapon_SO;
        if (savedWeapon == null) { return; }

        silencer_SO = savedWeapon.silencer_SO;
        weaponName = m_wearableItemsInventory.WeaponSlot.Item.gameObject.name;
        ammoCount = savedWeapon.ammoCount;
        cartridgeСlipAmmo = savedWeapon.clipAmmo;
    }

    public override void Load()
    {
        //if (m_equipmentInventory.WeaponSlot.Item != null && m_equipmentInventory.WeaponSlot.Item != savedWeapon) { m_equipmentInventory.WeaponSlot.Clear(); }

        bool didPlayerTakeWeapon = savedWeapon != null;

        Weapon_SO weapon = m_wearableItemsInventory.WeaponSlot.Item as Weapon_SO;
        if (weapon != null)
        {
            if (!didPlayerTakeWeapon)
            {
                weapon.gameObject.SetActive(true);
            }

            if (m_weaponActivator.IsWeaponActive) { m_weaponActivator.SetWeaponActiveState(false); }
            weapon.IsInInventory = false;
            m_wearableItemsInventory.WeaponSlot.Clear();
        }

        if (!didPlayerTakeWeapon) { return; }

        m_wearableItemsInventory.WeaponSlot.SetItem(savedWeapon);
        LoadWeaponData();
    }

    void LoadWeaponData()
    {
        savedWeapon.IsInInventory = true;
        savedWeapon.ammoCount = ammoCount;
        savedWeapon.clipAmmo = cartridgeСlipAmmo;
        m_weaponActivator.SetWeaponActiveState(isActive);

        if (silencer_SO != null) { silencer_SO.Equip(); }
    }

    public override void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(weaponName)) { return; }

        print(silencer_SO.name);
        GameObject weaponGameObject = PropsHandler.Find(weaponName).gameObject;

        weaponGameObject.GetComponent<ItemHandler>().Interact();
        LoadWeaponData();
    }
}
