using System;
using UnityEngine;

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

    void Update()
    {
        if (!Input.GetKeyDown(m_key)) { return; }

        SwitchInjectType();
    }

    void SwitchInjectType()
    {
        if (Type == typeof(IHealthInjectable))
        {
            Type = typeof(IAdrenalinInjectable);
            return;
        }

        Type = typeof(IHealthInjectable);
    }
}