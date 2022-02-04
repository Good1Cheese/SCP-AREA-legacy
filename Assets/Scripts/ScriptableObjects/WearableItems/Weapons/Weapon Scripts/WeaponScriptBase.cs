using UnityEngine;
using Zenject;

public abstract class WeaponScriptBase : MonoBehaviour, IInteractable
{
    protected WeaponSlot _weaponSlot;
    protected PauseMenuToggler _pauseMenuToggler;
    protected PickableInventoryToggler _pickableInventoryToggler;
    protected WeaponRequestsHandler _weaponRequestsHandler;
    protected WeaponHandler _weaponHandler;

    public abstract WaitForSeconds RequestTimeout { get; }
    public abstract AudioClip RequestClip { get; }
    public virtual bool Interuppable => false;

    [Inject]
    private void Inject(WeaponSlot weaponSlot,
                        PauseMenuToggler pauseMenuToggler,
                        PickableInventoryToggler pickableInventoryToggler,
                        WeaponRequestsHandler weaponRequestsHandler)
    {
        _weaponSlot = weaponSlot;
        _pickableInventoryToggler = pickableInventoryToggler;
        _pauseMenuToggler = pauseMenuToggler;
        _weaponRequestsHandler = weaponRequestsHandler;
    }

    protected void Start()
    {
        _weaponSlot.Changed += SetWeaponHandler;
        _weaponSlot.ItemRemoved += SetWeaponHandlerToNull;
    }

    protected virtual void SetWeaponHandler(WeaponHandler weaponHandler) => _weaponHandler = weaponHandler;
    private void SetWeaponHandlerToNull() => _weaponHandler = null;

    protected bool CanNotWeaponDoAction()
    {
        return _weaponHandler == null
            || !_weaponHandler.GameObjectForPlayer.activeSelf;
    }

    protected void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponHandler;
        _weaponSlot.ItemRemoved -= SetWeaponHandlerToNull;
    }

    public virtual void Interact() { }
    public virtual void OnSuccesRequest() { }
}