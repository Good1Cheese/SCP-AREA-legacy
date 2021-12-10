using UnityEngine;
using Zenject;

[RequireComponent(typeof(InjectTypeSwitch), typeof(InjectShoot))]
public class InjectorReload : InjectorAction
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    private InjectTypeSwitch _injectTypeSwitcher;

    public ItemHandler CurrentInject
    {
        set
        {
            if (value == null) { return; }

            print(value);
            InjectableItemHandler injectableItemHandler = (InjectableItemHandler)value;
            injectableItemHandler.NumsOfUses--;

            _injectorHandler.ClipInject = injectableItemHandler;
        }
    }

    private new void Start()
    {
        base.Start();

        _injectTypeSwitcher = GetComponent<InjectTypeSwitch>();
    }

    protected override void DoAction()
    {
        _injectorSlot.StartItemAction(_injectorHandler.Injector_SO.reloadTimeout);

        if (_injectTypeSwitcher.CurrentType == typeof(IHealthInjectable))
        {
            CurrentInject = _pickableItemsInventory.GetIem(item => item as IHealthInjectable != null);
            return;
        }

        CurrentInject = _pickableItemsInventory.GetIem(item => item as IAdrenalinInjectable != null);
    }
}