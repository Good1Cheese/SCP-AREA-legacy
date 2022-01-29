using System;
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
            var injectableItemHandler = (IInjectable)value;

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
            CurrentInject = GetInject(item => item as IHealthInjectable != null);
            return;
        }

        CurrentInject = GetInject(item => item as IAdrenalinInjectable != null);
    }

    public ItemHandler GetInject(Predicate<ItemHandler> condition)
    {
        var injectables = Array.FindAll(_pickableItemsInventory.Inventory,
                                        injectable => condition.Invoke(injectable));
        for (int i = 0; i < injectables.Length; i++)
        {
            var itemHandler = (StackableItemHandler)injectables[i];
            StackableItemSlot slotWithItem = Array.Find(itemHandler.StackSlots.Slots, slot => slot.HasItem);

            if (slotWithItem == null) { continue; }

            return slotWithItem.GetItem();
        }
        return null;
    }
}