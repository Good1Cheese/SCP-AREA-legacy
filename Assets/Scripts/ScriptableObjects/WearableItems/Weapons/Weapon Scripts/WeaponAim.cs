using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponAction
{
    const KeyCode AIM_KEY = KeyCode.Mouse1;

    [Inject] readonly Animator m_weaponAnimator;
    [Inject] readonly WeaponReload m_weaponReload;

    public bool IsAiming { get; set; }

    public Action OnPlayerFiredWithAim { get; set; }
    public Action OnPlayerFiredWithoutAim { get; set; }

    public Action OnPlayerAimed { get; set; }
    public Action OnPlayerInTakedAim { get; set; }

    void Update()
    {
        if (m_weaponReload.IsPlayerReloading) { return; }

        if (Input.GetKeyDown(AIM_KEY))
        {
            SetAimState(true);
        }

        if (Input.GetKeyUp(AIM_KEY))
        {
            if (!m_weaponAnimator.GetBool("IsPlayerTakedAim")) { return; }

            SetAimState(false);
        }
    }

    public void SetAimState(bool isAiming)
    {
        IsAiming = isAiming;
        m_weaponAnimator.SetBool("IsPlayerTakedAim", isAiming);

        if(IsAiming)
        {
            OnPlayerAimed?.Invoke();
            return;
        }
        OnPlayerInTakedAim?.Invoke();
    }


    protected new void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }
}
