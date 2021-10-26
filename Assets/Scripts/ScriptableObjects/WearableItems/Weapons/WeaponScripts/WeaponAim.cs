﻿using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponAction
{
    const KeyCode AIM_KEY = KeyCode.Mouse1;

    [Inject] readonly Animator m_weaponAnimator;
    [Inject] readonly WeaponReload m_weaponReload;

    public Action OnPlayerShootedWithAim { get; set; }
    public Action OnPlayerShootedWithoutAim { get; set; }

    public Action OnPlayerAimed { get; set; }
    public Action OnPlayerInTakedAim { get; set; }

    void Update()
    {
        if (m_weaponReload.IsPlayerReloading) { return; }

        if (Input.GetKeyDown(AIM_KEY))
        {
            OnPlayerAimed?.Invoke();
            m_weaponAnimator.SetBool("IsPlayerTakedAim", true);
        }

        if (Input.GetKeyUp(AIM_KEY))
        {
            if (!m_weaponAnimator.GetBool("IsPlayerTakedAim")) { return; }

            OnPlayerInTakedAim?.Invoke();
            m_weaponAnimator.SetBool("IsPlayerTakedAim", false);
        }
    }

    protected new void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }
}