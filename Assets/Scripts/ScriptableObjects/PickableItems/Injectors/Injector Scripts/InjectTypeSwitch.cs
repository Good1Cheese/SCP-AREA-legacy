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

    private void Switch()
    {
        if (CurrentType == typeof(IHealthInjectable))
        {
            CurrentType = typeof(IAdrenalinInjectable);
            return;
        }

        CurrentType = typeof(IHealthInjectable);
    }

    public override void Interact() => Switch();
}