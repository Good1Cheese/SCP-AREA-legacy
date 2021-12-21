using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponScriptBase
{
    private const KeyCode AI_KEY = KeyCode.Mouse1;

    [Inject] private readonly Animator _weaponAnimator;
    [Inject] private readonly WeaponReloadCoroutineUser _weaponReloadCoroutineUser;
    public bool IsAiming { get; set; }

    public Action FiredWithAim { get; set; }
    public Action FiredWithoutAim { get; set; }

    public Action Aimed { get; set; }
    public Action Unaimed { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(AI_KEY))
        {
            SetAimState(true);
        }

        if (Input.GetKeyUp(AI_KEY))
        {
            if (!_weaponAnimator.GetBool("IsPlayerTakedAim")) { return; }

            SetAimState(false);
        }
    }

    public void SetAimState(bool isAiming)
    {
        if (_weaponReloadCoroutineUser.IsActionGoing) { return; }

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