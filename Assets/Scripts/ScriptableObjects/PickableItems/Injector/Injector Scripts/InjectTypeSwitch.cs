using System;

public class InjectTypeSwitch : InjectorScriptBase
{
    private Type _defaultType = typeof(IHealthInjectable);

    public Type CurrentType
    {
        get => _defaultType;
        set
        {
            print("Изменен тип инъекции " + value);
            _defaultType = value;
        }
    }

    protected override void DoAction()
    {
        _itemActionCreator.StartItemAction(_injectorHandler.Injector_SO.injectChangeTimeout, null);

        if (CurrentType == typeof(IHealthInjectable))
        {
            CurrentType = typeof(IAdrenalinInjectable);
            return;
        }

        CurrentType = typeof(IHealthInjectable);
    }
}