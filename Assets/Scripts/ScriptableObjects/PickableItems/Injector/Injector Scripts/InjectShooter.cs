public class InjectShooter : InjectorScriptBase
{
    private InjectorReload _injectorReload;

    private new void Start()
    {
        base.Start();
        _injectorReload = GetComponent<InjectorReload>();
    }

    protected override void DoScriptAction()
    {
        WearableItemsInventory.InjectorSlot.StartItemAction(InjectorHandler.Injector_SO.injectChangeTimeout);

        if (_injectorReload.InjectorHandler.ClipInject == null) { return; }

        _injectorReload.InjectorHandler.ClipInject.Inject();
        _injectorReload.InjectorHandler.ClipInject = null;
    }
}
