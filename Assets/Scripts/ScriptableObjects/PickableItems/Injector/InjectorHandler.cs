using UnityEngine;
using Zenject;

public class InjectorHandler : WearableItemHandler, IClickable
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly InjectorSlot _injectorSlot;

    public IInjectable ClipInject { get; set; }
    public Injector_SO Injector_SO => (Injector_SO)Item_SO;

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

    public override void Interact()
    {
        if (!_pickableItemsInventory.HasInventoryEnoughSpace(1)) { return; }

        base.Interact();
    }

    public void Clicked(int slotIndex)
    {
        _injectorSlot.Used?.Invoke();
    }

    public override void Dropped()
    {
        _injectorSlot.ClearSlot();
    }
}