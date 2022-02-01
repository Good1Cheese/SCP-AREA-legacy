using UnityEngine;

[RequireComponent(typeof(ReloadToggle), typeof(AmmoAmountToggle))]
public class ReloadOrAmmoCountToggler : WeaponScriptBase
{
    protected const KeyCode RELOAD_KEY = KeyCode.R;

    [SerializeField] protected float _pressTimeToActivate;

    private int _deltaTimeMultiplier = 1;

    public float PressTime { get; set; }
    public bool AmmoShowed { get; set; }
    public AmmoAmountToggle AmmoAmountToggle { get; set; }
    public ReloadToggle ReloadToggle { get; set; }

    private void Update()
    {
        if (Input.GetKey(RELOAD_KEY))
        {
            PressTime += Time.deltaTime * _deltaTimeMultiplier;
        }

        Toggle();
    }

    private void Toggle()
    {
        Reload();
        ShowAmmoCount();
    }

    private void Reload()
    {
        if (!Input.GetKeyUp(RELOAD_KEY)) { return; }

        if (AmmoShowed)
        {
            AmmoAmountToggle.Toggle(false);
            return;
        }


        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo) { return; }

        _weaponRequestsHandler.Handle(ReloadToggle, _weaponHandler.Weapon_SO.reloadTimeout);
    }

    private void ShowAmmoCount()
    {
        if (PressTime <= _pressTimeToActivate) { return; }

        AmmoAmountToggle.Interact();
    }

    public void LastActionWasReload(bool value)
    {
        AmmoShowed = value;
        _deltaTimeMultiplier = value ? 0 : 1;
    }
}