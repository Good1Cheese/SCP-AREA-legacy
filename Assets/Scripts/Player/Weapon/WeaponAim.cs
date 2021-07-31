using System;
using UnityEngine;
using Zenject;

public class WeaponAim : WeaponAction
{
    [Inject] readonly Animator m_weaponAnimator;

    public Action OnPlayerAimed { get; set; }
    public Action OnPlayerInTakedAim { get; set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnPlayerAimed?.Invoke();
            m_weaponAnimator.SetBool("IsPlayerTakedAim", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            OnPlayerInTakedAim?.Invoke();
            m_weaponAnimator.SetBool("IsPlayerTakedAim", false);
        }

    }

    protected new void SetWeapon(Weapon_SO weapon)
    {
        m_weaponAnimator.runtimeAnimatorController = weapon.weaponAnimationContoller;
    }
}
