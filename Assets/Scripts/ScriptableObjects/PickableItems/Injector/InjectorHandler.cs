using UnityEngine;
using Zenject;

public class InjectorHandler : WearableItemHandler, IClickable
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly InventoryEnablerDisabler _inventoryEnablerDisabler;
    [Inject] private readonly InjectorSlot _injectorSlot;

    public IInjectable ClipInject { get; set; }
    public Injector_SO Injector_SO => (Injector_SO)Item;

    private new void Start()
    {
        GameObject = gameObject;

        InjectorReload injectorReloader = GameObjectForPlayer.GetComponent<InjectorReload>();
        injectorReloader.PickableItemsInventory = _pickableItemsInventory;

        foreach (InjectorScriptBase i in GameObjectForPlayer.GetComponents<InjectorScriptBase>())
        {
            i.InventoryEnablerDisabler = _inventoryEnablerDisabler;
            i.InjectorSlot = _injectorSlot;
            i.InjectorHandler = this;
        }
    }

    private new void Awake()
    {
        base.Awake();

        if (Injector_SO.reloadTimeout != null) { return; }

        Injector_SO.reloadTimeout = new WaitForSeconds(Injector_SO.reloadDelay);
        Injector_SO.shotTimeout = new WaitForSeconds(Injector_SO.shotDelay);
        Injector_SO.injectChangeTimeout = new WaitForSeconds(Injector_SO.injectChangeDelay);
    }

    public override void Equip()
    {
        _injectorSlot.SetItem(this);
        _pickableItemsInventory.Add(this);
    }

    public void Clicked(int slotIndex)
    {
        _injectorSlot.OnSlotUsed?.Invoke();
    }

    public override void OnItemDropped()
    {
        _injectorSlot.ClearWearableSlot();
    }
}