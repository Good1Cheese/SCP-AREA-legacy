using UnityEngine;
using Zenject;

public class InjectorHandler : WearableItemHandler, IClickable
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly InventoryEnablerDisabler _inventoryEnablerDisabler;

    public IInjectable ClipInject { get; set; }
    public Injector_SO Injector_SO => (Injector_SO)GetItem();

    private new void Start()
    {
        GameObject = gameObject;

        InjectorReload injectorReloader = GameObjectForPlayer.GetComponent<InjectorReload>();
        injectorReloader.PickableItemsInventory = _pickableItemsInventory;

        foreach (InjectorScriptBase i in GameObjectForPlayer.GetComponents<InjectorScriptBase>())
        {
            i.InventoryEnablerDisabler = _inventoryEnablerDisabler;
            i.WearableItemsInventory = _wearableItemsInventory;
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
        _wearableItemsInventory.InjectorSlot.SetItem(this);
        _pickableItemsInventory.AddItem(this);
    }

    public void Clicked(int slotIndex)
    {
        _wearableItemsInventory.InjectorSlot.OnSlotUsed?.Invoke();
    }

    public override void OnItemDropped()
    {
        _wearableItemsInventory.InjectorSlot.ClearWearableSlot();
    }
}
