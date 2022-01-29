using Zenject;

public abstract class WeaponScriptBase : ItemScriptBase
{
    protected WeaponSlot _weaponSlot;
    protected PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    protected WeaponHandler _weaponHandler;

    [Inject]
    private void Inject(WeaponSlot weaponSlot, PauseMenuEnablerDisabler pauseMenuEnablerDisabler)
    {
        _itemSlot = weaponSlot;
        _weaponSlot = weaponSlot;
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
    }

    protected new void Start()
    {
        base.Start();

        _weaponSlot.Changed += SetWeaponHandler;
        _weaponSlot.ItemRemoved += SetWeaponHandlerToNull;
    }

    protected virtual void SetWeaponHandler(WeaponHandler weaponHandler) => _weaponHandler = weaponHandler;
    private void SetWeaponHandlerToNull() => _weaponHandler = null;

    protected bool CanNotWeaponDoAction()
    {
        return _pickableInventoryEnablerDisabler.IsActivated
            || _pauseMenuEnablerDisabler.IsActivated
            || _weaponHandler == null;
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();

        _weaponSlot.Changed -= SetWeaponHandler;
        _weaponSlot.ItemRemoved -= SetWeaponHandlerToNull;
    }
}