public class InjectShoot : InjectorAction
{
    protected override void DoAction()
    {
        _injectorSlot.StartItemAction(_injectorHandler.Injector_SO.injectChangeTimeout);

        if (_injectorHandler.ClipInject == null) { return; }

        print("Shooted " + _injectorHandler.ClipInject);

        _injectorHandler.ClipInject.Inject();
        _injectorHandler.ClipInject = null;
    }
}