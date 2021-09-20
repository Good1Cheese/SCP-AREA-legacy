using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponAction
{
    [Inject] readonly Animator m_weaponAnimator;

    public Action OnPlayerShootedWithAim { get; set; }
    public Action OnPlayerShootedWithoutAim { get; set; }

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
