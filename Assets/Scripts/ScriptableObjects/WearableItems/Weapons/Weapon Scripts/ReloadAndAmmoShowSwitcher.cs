using UnityEngine;
using Zenject;

public class ReloadAndAmmoShowSwitcher : WeaponAction
{
    private const KeyCode RELOAD_KEY = KeyCode.R;

    [SerializeField] private float _pressTimeToActivate;
    [Inject] private readonly WeaponReload _weaponReload;
    [Inject] private readonly AmmoUIEnablerDisabler _ammoUIEnablerDisabler;

    private bool _isSwitched;
    private float _pressTime;
    private sbyte _deltaTimeMultipliyer = 1;

    private void Update()
    {
        if (Input.GetKeyDown(RELOAD_KEY))
        {
            _isSwitched = true;
        }

        if (!Input.GetKeyUp(RELOAD_KEY)) { return; }

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
            _pressTime += Time.deltaTime * _deltaTimeMultipliyer;
        }

        if (_pressTime < _pressTimeToActivate || _weaponReload.IsReloading) { return; }

        _deltaTimeMultipliyer = 0;
        _pressTime = 0;

        _isSwitched = false;
        _weaponHandler.UpdateAmmo();
        _ammoUIEnablerDisabler.ActiveOrDisableUI(true);
    }
}