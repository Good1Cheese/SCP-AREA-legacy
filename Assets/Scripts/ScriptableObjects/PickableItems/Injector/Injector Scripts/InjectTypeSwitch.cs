using System;

public class InjectTypeSwitch : InjectorScriptBase
{
    private Type type = typeof(IHealthInjectable);

    public Type Type
    {
        get => type;
        set
        {
            print(value);
            type = value;
        }
    }

    protected override void DoScriptAction()
    {
        InjectorSlot.StartItemAction(InjectorHandler.Injector_SO.injectChangeTimeout);

        if (Type == typeof(IHealthInjectable))
        {
            Type = typeof(IAdrenalinInjectable);
            return;
        }

        Type = typeof(IHealthInjectable);
    }
}