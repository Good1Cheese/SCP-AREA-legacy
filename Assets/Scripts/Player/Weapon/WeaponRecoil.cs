using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class WeaponRecoil : WeaponAction
{
    [Inject] readonly Animator m_weaponAnimator;
    [Inject] readonly WeaponFire weaponFire;

    void Awake()
    {
        weaponFire.OnPlayerShootedWithAim += ActivateRecoilInAim;
        weaponFire.OnPlayerShootedWithoutAim += ActivateRecoilWithoutAim;
    }

    void ActivateRecoilInAim()
    {
        m_weaponAnimator.SetTrigger("OnPlayerShootedWithAim");
    }
    void ActivateRecoilWithoutAim()
    {
        m_weaponAnimator.SetTrigger("OnPlayerShootedWithoutAim");
    }

    protected override void SetWeapon(Weapon_SO weapon)
    {
        m_weaponAnimator.runtimeAnimatorController = weapon.weaponAnimationContoller;
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        weaponFire.OnPlayerShootedWithAim -= ActivateRecoilInAim;
        weaponFire.OnPlayerShootedWithoutAim -= ActivateRecoilWithoutAim;
    }
}
