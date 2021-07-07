using Zenject;

public class KeyCardCell : WearableItemSlot
{
    protected override void AddToEquipmentInventory()
    {
        m_equipmentInventory.KeyCardHandler = this;
    }
}