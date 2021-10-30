using UnityEngine;
using Zenject;

public class InjectorHandler : WearableItemHandler, IClickable
{
    [Inject] readonly PickableItemsInventory m_pickableItemsInventory;
    [Inject] readonly InventoryEnablerDisabler m_inventoryEnablerDisabler;

    public IInjectable ClipInject { get; set; }
    public Injector_SO Injector_SO => (Injector_SO)GetItem();

    new void Start()
    {
        GameObject = gameObject;

        InjectorReload injectorReloader = GameObjectForPlayer.GetComponent<InjectorReload>();
        injectorReloader.PickableItemsInventory = m_pickableItemsInventory;

        foreach (var i in GameObjectForPlayer.GetComponents<InjectorScriptBase>())
        {
            i.InventoryEnablerDisabler = m_inventoryEnablerDisabler;
            i.WearableItemsInventory = m_wearableItemsInventory;
            i.InjectorHandler = this;
        }
    }

    new void Awake()
    {
        base.Awake();

        if (Injector_SO.reloadTimeout != null) { return; }

        Injector_SO.reloadTimeout = new WaitForSeconds(Injector_SO.reloadDelay);
        Injector_SO.shotTimeout = new WaitForSeconds(Injector_SO.shotDelay);
        Injector_SO.injectChangeTimeout = new WaitForSeconds(Injector_SO.injectChangeDelay);
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
