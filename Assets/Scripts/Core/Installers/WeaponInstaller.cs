using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponActivator), typeof(WeaponShot))]
public class WeaponInstaller : MonoInstaller
{
    WeaponActivator m_weaponActivator;
    WeaponFire m_weaponFire;
    WeaponReload m_weaponReload;
    WeaponAim m_weaponAiming;
    Animator m_weaponAnimator;
    WeaponMiss m_weaponAmmoController;
    RayForShootingProvider m_rayForShootingProvider;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_weaponActivator).AsSingle();
        Container.BindInstance(m_weaponFire).AsSingle();
        Container.BindInstance(m_weaponReload).AsSingle();
        Container.BindInstance(m_weaponAiming).AsSingle();
        Container.BindInstance(m_weaponAnimator).AsSingle();
        Container.BindInstance(m_weaponAmmoController).AsSingle();
        Container.BindInstance(m_rayForShootingProvider).AsSingle();
    }

    void GetComponents()
    {
        m_rayForShootingProvider = GetComponent<RayForShootingProvider>();
        m_weaponAmmoController = GetComponent<WeaponMiss>();
        m_weaponActivator = GetComponent<WeaponActivator>();
        m_weaponFire = GetComponent<WeaponFire>();
        m_weaponReload = GetComponent<WeaponReload>();
        m_weaponAiming = GetComponent<WeaponAim>();
        m_weaponAnimator = GetComponent<Animator>();
    }
}
