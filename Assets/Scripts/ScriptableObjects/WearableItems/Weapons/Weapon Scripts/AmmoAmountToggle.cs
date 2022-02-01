using Zenject;

public class AmmoAmountToggle : WeaponScriptBase, IInteractable
{
    private ReloadOrAmmoCountToggler _reloadOrAmmoCountToggler;
    private WeaponReload _weaponReload;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;

    [Inject]
    private void Inject(AmmoUIEnablerDisabler ammoUIEnablerDisabler, WeaponReload weaponReload)
    {
        _ammoUIEnablerDisabler = ammoUIEnablerDisabler;
        _weaponReload = weaponReload;
    }

    private void Awake()
    {
        _reloadOrAmmoCountToggler = GetComponent<ReloadOrAmmoCountToggler>();
        _reloadOrAmmoCountToggler.AmmoAmountToggle = this;
    }

    public void Interact()
    {
        if (_weaponReload.IsCoroutineGoing) { return; }

        Toggle(true);
    }

    public void Toggle(bool value)
    {
        _ammoUIEnablerDisabler.EnableDisable(value);
        _reloadOrAmmoCountToggler.LastActionWasReload(value);
        _reloadOrAmmoCountToggler.PressTime = 0;
    }
}