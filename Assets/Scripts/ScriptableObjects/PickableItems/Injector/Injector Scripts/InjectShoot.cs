public class InjectShoot : InjectorAction
{
    protected override void DoAction()
    {
        _itemActionCreator.StartItemAction(_injectorHandler.Injector_SO.injectChangeTimeout, null);

        if (_injectorHandler.ClipInject == null) { return; }

        print("Shooted " + _injectorHandler.ClipInject);

        _injectorHandler.ClipInject.Inject();
        _injectorHandler.ClipInject = null;
    }
}