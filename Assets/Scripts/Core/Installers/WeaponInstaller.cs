using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponActivator), typeof(WeaponShot), typeof(WeaponShot))]
public class WeaponInstaller : MonoInstaller
{
    private AmmoMixup _ammoMixup;
    private WeaponFire _weaponFire;
    private WeaponShot _weaponShot;
    private WeaponReload _weaponReload;
    private WeaponReloadCoroutineUser _weaponReloadCoroutineUser;
    private WeaponAim _weaponAiming;
    private Animator _weaponAnimator;
    private WeaponMiss _weaponAmmoController;
    private RayForFireProvider _rayForShootingProvider;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(_ammoMixup).AsSingle();
        Container.BindInstance(_weaponFire).AsSingle();
        Container.BindInstance(_weaponShot).AsSingle();
        Container.BindInstance(_weaponReload).AsSingle();
        Container.BindInstance(_weaponReloadCoroutineUser).AsSingle();
        Container.BindInstance(_weaponAiming).AsSingle();
        Container.BindInstance(_weaponAnimator).AsSingle();
        Container.BindInstance(_weaponAmmoController).AsSingle();
        Container.BindInstance(_rayForShootingProvider).AsSingle();
    }

    private void GetComponents()
    {
        _ammoMixup = GetComponent<AmmoMixup>();
        _weaponFire = GetComponent<WeaponFire>();
        _weaponShot = GetComponent<WeaponShot>();
        _weaponReload = GetComponent<WeaponReload>();
        _weaponReloadCoroutineUser = GetComponent<WeaponReloadCoroutineUser>();
        _weaponAiming = GetComponent<WeaponAim>();
        _weaponAnimator = GetComponent<Animator>();
        _weaponAmmoController = GetComponent<WeaponMiss>();
        _rayForShootingProvider = GetComponent<RayForFireProvider>();
    }
}
