using UnityEngine;
using Zenject;

public class WeaponDataSaving : DataHandler
{
    [Inject] readonly WearableItemsInventory m_equipmentInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    GameObject weapon;

    public Item_SO savedWeapon;
    public bool isActive;
    public int ammoCount;
    public int cartridgeСlipAmmo;
    public string weaponName;

    public Transform PropsHandler { get; set; }

    public override void SaveData()
    {
        isActive = m_weaponActivator.IsWeaponActive;

        Weapon_SO weapon_SO = m_equipmentInventory.WeaponSlot.Item as Weapon_SO;
        if (weapon_SO == null) { return; }

        savedWeapon = weapon_SO;
        ammoCount = weapon_SO.ammoCount;
        cartridgeСlipAmmo = weapon_SO.cartridgeСlipAmmo;
        weaponName = m_equipmentInventory.WeaponSlot.Item.gameObject.name;
    }

    public override void LoadData()
    {
        //if (m_equipmentInventory.WeaponSlot.Item != null && m_equipmentInventory.WeaponSlot.Item != savedWeapon) { m_equipmentInventory.WeaponSlot.Clear(); }
        bool didPlayerTakeWeapon = savedWeapon != null;
        if (m_equipmentInventory.WeaponSlot.Item != null)
        {
            Weapon_SO weapon = m_equipmentInventory.WeaponSlot.Item as Weapon_SO;
            if (!didPlayerTakeWeapon)
            {
                weapon.gameObject.SetActive(true);
            }

            m_equipmentInventory.WeaponSlot.Clear();
        }

        if (!didPlayerTakeWeapon) { return; }

        Weapon_SO weapon_SO = savedWeapon as Weapon_SO;
        m_equipmentInventory.WeaponSlot.SetItem(weapon_SO);
        LoadWeaponData(weapon_SO);
    }

    void LoadWeaponData(Weapon_SO weapon_SO)
    {
        weapon_SO.ammoCount = ammoCount;
        weapon_SO.cartridgeСlipAmmo = cartridgeСlipAmmo;
        m_weaponActivator.SetWeaponActiveState(isActive);
    }

    public override void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(weaponName)) { return; }

        weapon = PropsHandler.Find(weaponName).gameObject;
        ItemHandler weaponItemHandler = weapon.GetComponent<ItemHandler>();

        weaponItemHandler.Interact();
        LoadWeaponData(weaponItemHandler.Item_SO as Weapon_SO);
    }
}
