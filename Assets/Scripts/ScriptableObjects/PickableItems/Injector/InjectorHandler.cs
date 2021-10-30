using System.Linq;
using Zenject;

public class InjectorHandler : WearableItemHandler, IClickable
{
    [Inject] readonly PickableItemsInventory m_pickableItemsInventory;
    [Inject] readonly InventoryEnablerDisabler m_inventoryEnablerDisabler;

    public IInjectable ClipInject { get; set; }

    new void Start()
    {
        GameObject = gameObject;

        InjectorReload injectorReloader = GameObjectForPlayer.GetComponent<InjectorReload>();
        injectorReloader.PickableItemsInventory = m_pickableItemsInventory;
        injectorReloader.InjectorHandler = this;

        foreach (var i in GameObjectForPlayer.GetComponents<InjectorScriptBase>())
        {
            i.InventoryEnablerDisabler = m_inventoryEnablerDisabler;
        }
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
