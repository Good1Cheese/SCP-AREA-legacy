using UnityEngine;

[RequireComponent(typeof(InjectTypeSwitch), typeof(InjectShooter))]
public class InjectorReload : InjectorScriptBase
{
    InjectTypeSwitch m_injectTypeSwitcher;

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

    protected override void DoScriptAction()
    {
        WearableItemsInventory.InjectorSlot.StartItemAction(InjectorHandler.Injector_SO.reloadTimeout);

        if (m_injectTypeSwitcher.Type == typeof(IHealthInjectable))
        {
            CurrentInject = PickableItemsInventory.GetIem(item => item as IHealthInjectable != null);
            return;
        }

        CurrentInject = PickableItemsInventory.GetIem(item => item as IAdrenalinInjectable != null);
    }
}
