using Zenject;

public class ReloadToggle : WeaponScriptBase, IInteractable
{
    private ReloadOrAmmoCountToggler _reloadOrAmmoCountToggler;
    protected WeaponReload _weaponReload;

    [Inject]
    private void Inject(WeaponReload weaponReload)
    {
        _weaponReload = weaponReload;
    }

    private void Awake()
    {
        _reloadOrAmmoCountToggler = GetComponent<ReloadOrAmmoCountToggler>();
        _reloadOrAmmoCountToggler.ReloadToggle = this;
    }

    public void Interact()
    {
        _weaponReload.StartWithoutInterrupt();
        _reloadOrAmmoCountToggler.PressTime = 0;
    }
}