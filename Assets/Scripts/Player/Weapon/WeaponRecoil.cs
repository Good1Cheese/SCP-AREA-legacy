using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class WeaponRecoil : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly Animator m_weaponAnimator;
    [Inject] readonly WeaponAim m_weaponAim;

    void Awake()
    {
        m_weaponAim.OnPlayerShootedWithAim += ActivateRecoilInAim;
        m_weaponAim.OnPlayerShootedWithoutAim += ActivateRecoilWithoutAim;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    void ActivateRecoilInAim()
    {
        m_weaponAnimator.SetTrigger("OnPlayerShootedWithAim");
    }

    void ActivateRecoilWithoutAim()
    {
        m_weaponAnimator.SetTrigger("OnPlayerShootedWithoutAim");
    }

    void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }

    void OnDestroy()
    {
        m_weaponAim.OnPlayerShootedWithAim -= ActivateRecoilInAim;
        m_weaponAim.OnPlayerShootedWithoutAim -= ActivateRecoilWithoutAim;
    }
}
