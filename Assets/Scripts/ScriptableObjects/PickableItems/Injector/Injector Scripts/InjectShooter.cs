public class InjectShooter : InjectorScriptBase
{
    InjectorReload m_injectorReload;

    new void Start()
    {
        base.Start();
        m_injectorReload = GetComponent<InjectorReload>();
    }

    protected override void DoScriptAction()
    {
        WearableItemsInventory.InjectorSlot.StartItemAction(InjectorHandler.Injector_SO.injectChangeTimeout);

        if (m_injectorReload.InjectorHandler.ClipInject == null) { return; }

        m_injectorReload.InjectorHandler.ClipInject.Inject();
        m_injectorReload.InjectorHandler.ClipInject = null;
    }
}
