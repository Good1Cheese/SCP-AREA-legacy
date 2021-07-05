using Zenject;

public class WeaponCell : WearableItemSlot
{
    [Inject]
    protected override void AddToEquipmentInventory(EquipmentInventory m_equipmentInventory)
    {
        m_equipmentInventory.WeaponHandler = this;
    }
}
