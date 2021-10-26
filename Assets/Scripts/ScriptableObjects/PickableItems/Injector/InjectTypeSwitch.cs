using System;
using UnityEngine;

public class InjectTypeSwitch : MonoBehaviour
{
    [SerializeField] KeyCode m_key;
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
        if (!Input.GetKeyDown(m_key) || transform.parent == null) { return; }

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