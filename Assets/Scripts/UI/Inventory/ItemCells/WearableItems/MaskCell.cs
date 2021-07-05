using Zenject;

public class MaskCell : WearableItemSlot
{
    [Inject]
    protected override void AddToEquipmentInventory(EquipmentInventory m_equipmentInventory)
    {
        m_equipmentInventory.MaskHandler = this;
    }
}
