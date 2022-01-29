using UnityEngine;
using Zenject;

public class InjectorHandler : WearableItemHandler
{
    private PickableItemsInventory _pickableItemsInventory;

    public IInjectable ClipInject { get; set; }
    public Injector_SO Injector_SO => (Injector_SO)Item_SO;

    [Inject]
    private void Construct(InjectorSlot injectorSlot, PickableItemsInventory pickableItemsInventory)
    {
        _wearableSlot = injectorSlot;
        _pickableItemsInventory = pickableItemsInventory;
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
        _wearableSlot.SetItem(this);
        _pickableItemsInventory.Add(this);
    }

    public override void Interact()
    {
        if (!_pickableItemsInventory.HasEnoughSpaceForItem()) { return; }

        base.Interact();
    }

    public override void Dropped()
    {
        _wearableSlot.ClearSlot();
    }
}