public class InjectorActivator : WearableItemActivator
{
    protected override WearableItemSlot WearableItemSlot => m_wearableItemsInventory.InjectorSlot;

    new void Start()
    {
        base.Start();
        m_wearableItemsInventory.InjectorSlot.OnSlotUsed += ActivateItem;
    }

    void ActivateItem()
    {
        SetItemActiveState(true);
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        m_wearableItemsInventory.InjectorSlot.OnSlotUsed -= ActivateItem;
    }
}
