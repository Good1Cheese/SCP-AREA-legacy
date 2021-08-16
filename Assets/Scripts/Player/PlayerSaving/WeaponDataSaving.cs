using Zenject;

public class WeaponDataSaving : DataHandler
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public Item_SO savedWeapon;
    public bool isWeaponActive;

    public override void SaveData()
    {
        isWeaponActive = m_weaponActivator.IsWeaponActive;
        savedWeapon = m_equipmentInventory.WeaponSlot.Item;
    }

    public override void LoadData()
    {
        m_equipmentInventory.WeaponSlot.ClearSlot();
        if (savedWeapon != null && m_equipmentInventory.WeaponSlot.Item == null)
        {
            m_equipmentInventory.WeaponSlot.SetItem(savedWeapon);
        }
    }

}
