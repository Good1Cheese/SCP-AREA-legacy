using UnityEngine;
using Zenject;

public class ReloadAndAmmoShowSwitcher : WeaponScriptBase
{
    private const KeyCode RELOAD_KEY = KeyCode.R;

    [SerializeField] private float _pressTimeToActivate;

    private WeaponReload _weaponReload;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;

    private bool _isSwitched;
    private float _pressTime;
    private sbyte _deltaTimeMultipliyer = 1;


    [Inject]
    private void Inject(WeaponReload weaponReload, AmmoUIEnablerDisabler ammoUIEnablerDisabler)
    {
        _weaponReload = weaponReload;
        _ammoUIEnablerDisabler = ammoUIEnablerDisabler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(RELOAD_KEY))
        {
            if (CanNotWeaponDoAction()) { return; }

            _isSwitched = true;
        }

        if (!Input.GetKeyUp(RELOAD_KEY)) { return; }

        if (CanNotWeaponDoAction()) { return; }

        _pressTime = 0;
        _deltaTimeMultipliyer = 1;

        if (_isSwitched)
        {
            _weaponReload.ActivateReload();
            return;
        }

        _ammoUIEnablerDisabler.ActiveOrDisableUI(false);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(RELOAD_KEY))
        {
            if (CanNotWeaponDoAction()) { return; }

            _pressTime += Time.deltaTime * _deltaTimeMultipliyer;
        }

        if (_pressTime < _pressTimeToActivate || _weaponReload.IsCoroutineGoing) { return; }

        if (CanNotWeaponDoAction()) { return; }

        _deltaTimeMultipliyer = 0;
        _pressTime = 0;

        _isSwitched = false;
        _weaponHandler.UpdateAmmo();
        _ammoUIEnablerDisabler.ActiveOrDisableUI(true);
    }
}