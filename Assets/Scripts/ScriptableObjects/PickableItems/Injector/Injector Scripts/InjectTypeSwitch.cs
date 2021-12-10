using System;

public class InjectTypeSwitch : InjectorAction
{
    private Type _defaultType = typeof(IHealthInjectable);

    public Type CurrentType
    {
        get => _defaultType;
        set
        {
            print(value);
            _defaultType = value;
        }
    }

    protected override void DoAction()
    {
        _injectorSlot.StartItemAction(_injectorHandler.Injector_SO.injectChangeTimeout);

        if (CurrentType == typeof(IHealthInjectable))
        {
            CurrentType = typeof(IAdrenalinInjectable);
            return;
        }

        CurrentType = typeof(IHealthInjectable);
    }
}