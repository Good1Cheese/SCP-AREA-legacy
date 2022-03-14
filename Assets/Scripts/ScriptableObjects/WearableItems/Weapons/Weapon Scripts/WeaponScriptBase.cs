using UnityEngine;
using Zenject;

public abstract class WeaponScriptBase : WeaponUser, IInteractable
{
    protected PauseMenuToggler _pauseMenuToggler;
    protected PickableInventoryToggler _pickableInventoryToggler;
    protected WeaponRequestsHandler _weaponRequestsHandler;

    public abstract WaitForSeconds InteractionTimeout { get; }
    public abstract AudioClip Sound { get; }

    [Inject]
    private void Inject(WeaponSlot weaponSlot,
                        PauseMenuToggler pauseMenuToggler,
                        PickableInventoryToggler pickableInventoryToggler,
                        WeaponRequestsHandler weaponRequestsHandler)
    {
        _pickableInventoryToggler = pickableInventoryToggler;
        _pauseMenuToggler = pauseMenuToggler;
        _weaponRequestsHandler = weaponRequestsHandler;
    }

    protected new void Start()
    {
        base.Start();
        _weaponSlot.ItemRemoved += SetWeaponHandlerToNull;
    }

    private void SetWeaponHandlerToNull() => _weaponHandler = null;

    protected bool IsWeaponNotAvailable()
    {
        return _weaponHandler == null
            || !_weaponHandler.GameObjectForPlayer.activeSelf;
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();
        _weaponSlot.ItemRemoved -= SetWeaponHandlerToNull;
    }

    public virtual void Interact() { }
}