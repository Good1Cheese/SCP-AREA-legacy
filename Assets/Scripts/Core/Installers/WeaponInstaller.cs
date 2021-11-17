using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponActivator), typeof(WeaponShot), typeof(WeaponShot))]
public class WeaponInstaller : MonoInstaller
{
    private WeaponFire _weaponFire;
    private WeaponShot _weaponShot;
    private WeaponReload _weaponReload;
    private WeaponAim _weaponAiming;
    private Animator _weaponAnimator;
    private WeaponMiss _weaponAmmoController;
    private RayForFireProvider _rayForShootingProvider;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(_weaponFire).AsSingle();
        Container.BindInstance(_weaponShot).AsSingle();
        Container.BindInstance(_weaponReload).AsSingle();
        Container.BindInstance(_weaponAiming).AsSingle();
        Container.BindInstance(_weaponAnimator).AsSingle();
        Container.BindInstance(_weaponAmmoController).AsSingle();
        Container.BindInstance(_rayForShootingProvider).AsSingle();
    }

    private void GetComponents()
    {
        _weaponFire = GetComponent<WeaponFire>();
        _weaponShot = GetComponent<WeaponShot>();
        _weaponReload = GetComponent<WeaponReload>();
        _weaponAiming = GetComponent<WeaponAim>();
        _weaponAnimator = GetComponent<Animator>();
        _weaponAmmoController = GetComponent<WeaponMiss>();
        _rayForShootingProvider = GetComponent<RayForFireProvider>();
    }
}
