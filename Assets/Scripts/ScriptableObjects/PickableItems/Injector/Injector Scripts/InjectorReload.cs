using UnityEngine;
using Zenject;

[RequireComponent(typeof(InjectTypeSwitch), typeof(InjectShoot))]
public class InjectorReload : InjectorScriptBase
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    private InjectTypeSwitch _injectTypeSwitcher;

    public ItemHandler CurrentInject
    {
        set
        {
            if (value == null) { return; }

            print("Вставлен " + value);
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
        _itemActionCreator.StartItemAction(_injectorHandler.Injector_SO.reloadTimeout, null);

        if (_injectTypeSwitcher.CurrentType == typeof(IHealthInjectable))
        {
            CurrentInject = _pickableItemsInventory.GetIem(item => item as IHealthInjectable != null);
            return;
        }

        CurrentInject = _pickableItemsInventory.GetIem(item => item as IAdrenalinInjectable != null);
    }
}