using UnityEngine;
using Zenject;

public abstract class InjectorAction : ItemAction
{
    [SerializeField] protected KeyCode _key;

    [Inject] protected readonly InjectorSlot _injectorSlot;

    protected InjectorHandler _injectorHandler;

    protected override WearableSlot ItemSlot => _injectorSlot;
    protected override WearableItemHandler WearableItemHandler => _injectorHandler;

    protected new void Start()
    {
        base.Start();

        _injectorSlot.OnInjectorChanged += SetInjector;
    }

    protected void Update()
    {
        if (!Input.GetKeyDown(_key) || _injectorSlot.ItemActionMaker.IsItemActionGoing) { return; }

        DoAction();
    }

    private void SetInjector(InjectorHandler injectorHandler)
    {
        _injectorHandler = injectorHandler;
    }

    protected abstract void DoAction();

    protected new void OnDestroy()
    {
        base.OnDestroy();

        _injectorSlot.OnInjectorChanged -= SetInjector;
    }
}