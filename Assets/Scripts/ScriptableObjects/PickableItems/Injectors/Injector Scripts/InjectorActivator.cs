using Zenject;

public class InjectorActivator : WearableItemActivator
{
    [Inject] private readonly InjectorSlot _injectorSlot;

    private bool _activatedFromInventory;
    public override WearableSlot Slot => _injectorSlot;

    private new void Start()
    {
        base.Start();

        _injectorSlot.Used += InjectorInventoryActivation;
        _inventoryEnablerDisabler.EnabledDisabled += ActivateInjector;
    }

    private void InjectorInventoryActivation()
    {
        _activatedFromInventory = true;
        _wearableItemHandler.GameObjectForPlayer.SetActive(true);
    }

    private void ActivateInjector()
    {
        if (!_activatedFromInventory) { return; }

        SetWearableItemActiveState(true);
        _activatedFromInventory = false;
    }

    private new void OnDestroy()
    {
        base.OnDestroy();

        _injectorSlot.Used -= InjectorInventoryActivation;
    }
}