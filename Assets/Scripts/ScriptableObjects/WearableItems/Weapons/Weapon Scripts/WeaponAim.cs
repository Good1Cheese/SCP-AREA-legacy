using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponScriptBase
{
    private const KeyCode AIM_KEY = KeyCode.Mouse1;

    [Inject] private readonly Animator _weaponAnimator;
    [Inject] private readonly WeaponReloadCoroutineUser _weaponReloadCoroutineUser;

    public bool IsAiming { get; set; }

    public Action FiredWithAim { get; set; }
    public Action FiredWithoutAim { get; set; }

    public Action Aimed { get; set; }
    public Action Unaimed { get; set; }
    public bool WasAimed { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(AIM_KEY))
        {
            SetAimStateWithTriggerCheck(true);
        }

        if (Input.GetKeyUp(AIM_KEY))
        {
            if (!_weaponAnimator.GetBool("IsPlayerTakedAim")) { return; }

            SetAimState(false);
        }
    }

    public void SetAimStateWithTriggerCheck(bool isAiming)
    {
        WasAimed = _weaponHandler.ClippingMaker.GameObjectTrigger.IsTriggered;

        if (WasAimed) { return; }

        SetAimState(isAiming);
    }

    public void SetAimState(bool isAiming)
    {
        if (_weaponReloadCoroutineUser.IsCoroutineGoing
            || _inventoryEnablerDisabler.IsActivated
            || _pauseMenuEnablerDisabler.IsActivated) { return; }

        IsAiming = isAiming;
        _weaponAnimator.SetBool("IsPlayerTakedAim", isAiming);

        if (IsAiming)
        {
            Aimed?.Invoke();
            return;
        }
        Unaimed?.Invoke();
    }

    protected new void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }
}