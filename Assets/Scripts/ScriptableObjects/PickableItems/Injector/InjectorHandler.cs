using Zenject;

public class InjectorHandler : WearableItemHandler, IClickable
{
    [Inject] readonly PickableItemsInventory m_pickableItemsInventory;

    public IInjectable ClipInject { get; set; }

    new void Awake()
    {
        base.Awake();
        InjectorReload injectorReloader = GameObjectForPlayer.GetComponent<InjectorReload>();
        injectorReloader.PickableItemsInventory = m_pickableItemsInventory;
        injectorReloader.InjectorHandler = this;
    }

    public override void Equip()
    {
        m_wearableItemsInventory.InjectorSlot.SetItem(this);
        m_pickableItemsInventory.AddItem(this);
    }

    public void Clicked(int slotIndex)
    {
        m_wearableItemsInventory.InjectorSlot.OnSlotUsed?.Invoke();
    }

    public override void OnItemDropped()
    {
        m_wearableItemsInventory.InjectorSlot.ClearWearableSlot();
    }
}
