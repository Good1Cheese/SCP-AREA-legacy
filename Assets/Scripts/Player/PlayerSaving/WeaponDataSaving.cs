using UnityEngine;
using Zenject;

public class WeaponDataSaving : DataHandler
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    GameObject weapon;

    public Item_SO savedWeapon;
    public bool isWeaponActive;
    public string weaponName;

    public Transform PropsHandler { get; set; }

    public override void SaveData()
    {
        isWeaponActive = m_weaponActivator.IsWeaponActive;
        savedWeapon = m_equipmentInventory.WeaponSlot.Item;

        if (m_equipmentInventory.WeaponSlot.Item != null)
        {
            weaponName = m_equipmentInventory.WeaponSlot.Item.gameObject.name;
            print(weaponName);
        }
    }

    public override void LoadData()
    {
        if (savedWeapon != null && m_equipmentInventory.WeaponSlot.Item == null)
        {
            m_equipmentInventory.WeaponSlot.SetItem(savedWeapon);
        }
    }

    public override void FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        if (string.IsNullOrEmpty(weaponName)) { return; }

        weapon = PropsHandler.Find(weaponName).gameObject;
        weapon.GetComponent<ItemHandler>().Interact();
    }
}
