using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponScriptBase
{
    private const KeyCode AIM_KEY = KeyCode.Mouse1;

    private Animator _weaponAnimator;
    private WeaponReload _weaponReload;

    public bool IsAiming { get; set; }

    public Action FiredWithAim { get; set; }
    public Action FiredWithoutAim { get; set; }

    public Action Aimed { get; set; }
    public Action Unaimed { get; set; }
    public bool WasAimed { get; set; }

    [Inject]
    private void Inject(Animator weaponAnimator, WeaponReload weaponReload)
    {
        _weaponAnimator = weaponAnimator;
        _weaponReload = weaponReload;
    }

    private void Update()
    {
        if (Input.GetKeyDown(AIM_KEY))
        {
            SetAimStateWithTriggerCheck(true);
        }

        if (Input.GetKeyUp(AIM_KEY))
        {
            if (!_weaponAnimator.GetBool("Aimed")) { return; }

            SetAimState(false);
        }
    }

    public void SetAimStateWithTriggerCheck(bool isAiming)
    {
        if (_weaponHandler == null) { return; }

        WasAimed = _weaponHandler.ClippingMaker.GameObjectTrigger.IsTriggered;

        if (WasAimed) { return; }

        SetAimState(isAiming);
    }

    public void SetAimState(bool isAiming)
    {
        if (_weaponReload.IsCoroutineGoing
            || CanNotWeaponDoAction()) { return; }

        IsAiming = isAiming;
        _weaponAnimator.SetBool("Aimed", isAiming);

        if (IsAiming)
        {
            Aimed?.Invoke();
            return;
        }
        Unaimed?.Invoke();
    }

    protected override void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        base.SetWeaponHandler(weaponHandler);
        _weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }
}