using Zenject;

public class InjectorActivator : WearableItemActivator
{
    [Inject] private readonly InjectorSlot _injectorSlot;

    public override WearableSlot WearableItemSlot => _injectorSlot;

    private new void Start()
    {
        base.Start();
        _injectorSlot.Used += ActivateItem;
    }

    private void ActivateItem()
    {
        SetItemActiveState(true);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _injectorSlot.Used -= ActivateItem;
    }
}