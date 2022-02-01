public class InjectShoot : InjectorScriptBase
{
    private void Shoot()
    {
        if (_injectorHandler.ClipInject == null) { return; }

        print("Вставлен в игрока " + _injectorHandler.ClipInject);

        _injectorHandler.ClipInject.Inject();
        _injectorHandler.ClipInject = null;
    }

    public override void Interact() => Shoot();
}