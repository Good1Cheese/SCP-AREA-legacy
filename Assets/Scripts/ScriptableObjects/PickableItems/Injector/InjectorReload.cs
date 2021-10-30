using UnityEngine;

[RequireComponent(typeof(InjectTypeSwitch), typeof(InjectShooter))]
public class InjectorReload : InjectorScriptBase
{
    InjectTypeSwitch m_injectTypeSwitcher;

    public InjectorHandler InjectorHandler { get; set; } 
    public PickableItemsInventory PickableItemsInventory { get; set; }

    public ItemHandler CurrentInject
    {
        set
        {
            if (value == null) { return; }

            print(value.name);
            InjectableItemHandler injectableItemHandler = (InjectableItemHandler)value;
            injectableItemHandler.NumsOfUses--;

            InjectorHandler.ClipInject = injectableItemHandler;
        }
    }

    new void Start()
    {
        base.Start();
        m_injectTypeSwitcher = GetComponent<InjectTypeSwitch>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key)) { return; }

        Reload();
    }

    void Reload()
    {
        if (m_injectTypeSwitcher.Type == typeof(IHealthInjectable))
        {
            CurrentInject = PickableItemsInventory.GetIem(item => item as IHealthInjectable != null);
            return;
        }

        CurrentInject = PickableItemsInventory.GetIem(item => item as IAdrenalinInjectable != null);
    }
}
