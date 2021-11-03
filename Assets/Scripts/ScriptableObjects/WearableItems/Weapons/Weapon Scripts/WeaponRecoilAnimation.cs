using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class WeaponRecoilAnimation : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly Animator m_weaponAnimator;
    [Inject] readonly WeaponAim m_weaponAim;

    void Awake()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeaponAnimator;
        m_weaponAim.OnPlayerFiredWithAim += ActivateRecoilInAim;
        m_weaponAim.OnPlayerFiredWithoutAim += ActivateRecoilWithoutAim;
    }

    void ActivateRecoilInAim()
    {
        //m_weaponAnimator.SetTrigger("OnPlayerFiredWithAim");
    }

    void ActivateRecoilWithoutAim()
    {
        //m_weaponAnimator.SetTrigger("OnPlayerFiredWithoutAim");
    }

    void SetWeaponAnimator(WeaponHandler weaponHandler)
    {
        m_weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeaponAnimator;
        m_weaponAim.OnPlayerFiredWithAim -= ActivateRecoilInAim;
        m_weaponAim.OnPlayerFiredWithoutAim -= ActivateRecoilWithoutAim;
    }
}
