﻿using UnityEngine;
using Zenject;

public class ReloadOrAmmoCountToggler : WeaponScriptBase
{
    protected const KeyCode RELOAD_KEY = KeyCode.R;

    [SerializeField] protected float _pressTimeToActivate;

    private int _deltaTimeMultiplier = 1;
    private WeaponReload _weaponReload;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;

    public float PressTime { get; set; }
    public bool AmmoShowed { get; set; }

    public override WaitForSeconds RequestTimeout => null;
    public override AudioClip RequestClip => null;

    [Inject]
    private void Inject(AmmoUIEnablerDisabler ammoUIEnablerDisabler, WeaponReload weaponReload)
    {
        _ammoUIEnablerDisabler = ammoUIEnablerDisabler;
        _weaponReload = weaponReload;
    }

    private void Update()
    {
        if (CanNotWeaponDoAction()) { return; }

        if (Input.GetKey(RELOAD_KEY))
        {
            PressTime += Time.deltaTime * _deltaTimeMultiplier;
        }

        ToggleReload();
        ActivateAmmoCount();
    }

    private void ToggleReload()
    {
        if (!Input.GetKeyUp(RELOAD_KEY)) { return; }

        if (AmmoShowed)
        {
            ToggleAmmoCount(false);
            return;
        }

        if (_weaponReload.CurrentClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || !_weaponReload.HasAmmo) { return; }

        Reload();
    }

    private void Reload()
    {
        _weaponRequestsHandler.Handle(_weaponReload);
        PressTime = 0;
    }


    private void ActivateAmmoCount()
    {
        if (PressTime <= _pressTimeToActivate) { return; }

        ToggleAmmoCount(true);
    }

    public void ToggleAmmoCount(bool value)
    {
        _ammoUIEnablerDisabler.EnableDisable(value);
        AmmoShowed = value;
        _deltaTimeMultiplier = value ? 0 : 1;
        PressTime = 0;
    }
}