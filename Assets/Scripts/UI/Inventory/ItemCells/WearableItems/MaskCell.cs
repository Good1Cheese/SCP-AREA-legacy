using Zenject;

public class MaskCell : WearableItemSlot
{
    protected override void AddToEquipmentInventory()
    {
        m_equipmentInventory.MaskHandler = this;
    }
}
