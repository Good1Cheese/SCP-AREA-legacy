using UnityEngine;
using Zenject;

public abstract class WeaponScriptBase : MonoBehaviour
{
    protected WeaponSlot _weaponSlot;
    protected PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    protected PickableInventoryEnablerDisabler _pickableInventoryEnablerDisabler;
    protected WeaponRequestsHandler _weaponRequestsHandler;
    protected WeaponHandler _weaponHandler;

    [Inject]
    private void Inject(WeaponSlot weaponSlot,
                        PauseMenuEnablerDisabler pauseMenuEnablerDisabler,
                        PickableInventoryEnablerDisabler pickableInventoryEnablerDisabler,
                        WeaponRequestsHandler weaponRequestsHandler)
    {
        _weaponSlot = weaponSlot;
        _pickableInventoryEnablerDisabler = pickableInventoryEnablerDisabler;
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
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
        return _pickableInventoryEnablerDisabler.IsActivated
            || _pauseMenuEnablerDisabler.IsActivated
            || _weaponHandler == null
            || !_weaponHandler.GameObjectForPlayer.activeSelf;
    }

    protected void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponHandler;
        _weaponSlot.ItemRemoved -= SetWeaponHandlerToNull;
    }
}