using Zenject;

public class KeyCardCell : WearableItemSlot
{
    [Inject]
    protected override void AddToEquipmentInventory(EquipmentInventory m_equipmentInventory)
    {
        m_equipmentInventory.KeyCardHandler = this;
    }
}