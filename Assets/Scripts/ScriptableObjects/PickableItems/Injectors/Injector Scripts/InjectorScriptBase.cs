using UnityEngine;
using Zenject;

public abstract class InjectorScriptBase : ItemScriptBase
{
    [SerializeField] protected KeyCode _key;

    protected InjectorSlot _injectorSlot;
    protected InjectorHandler _injectorHandler;

    [Inject]
    private void Inject(InjectorSlot injectorSlot)
    {
        _itemSlot = injectorSlot;
        _injectorSlot = injectorSlot;
    }

    protected new void Start()
    {
        base.Start();

        _injectorSlot.Changed += SetInjector;
        _injectorSlot.ItemRemoved += SetInjectorToNull;
    }

    protected void Update()
    {
        if (!Input.GetKeyDown(_key)) { return; }

        if (_pickableInventoryEnablerDisabler.IsActivated) { return; }

        TryInteract();
    }

    private void SetInjector(InjectorHandler injectorHandler) => _injectorHandler = injectorHandler;
    private void SetInjectorToNull() => _injectorHandler = null;

    protected new void OnDestroy()
    {
        base.OnDestroy();

        _injectorSlot.Changed -= SetInjector; 
        _injectorSlot.ItemRemoved -= SetInjectorToNull;
    }
}