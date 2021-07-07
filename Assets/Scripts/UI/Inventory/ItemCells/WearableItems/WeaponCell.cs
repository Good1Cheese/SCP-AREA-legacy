using Zenject;

public class WeaponCell : WearableItemSlot
{
    protected override void AddToEquipmentInventory()
    {
        m_equipmentInventory.WeaponHandler = this;
    }
}
