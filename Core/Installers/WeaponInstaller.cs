using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponActivator), typeof(WeaponRecoil), typeof(WeaponSpawnerAndDestroyer))]
public class WeaponInstaller : MonoInstaller
{
    public WeaponActivator WeaponActivator { get; set; }
    public WeaponFire WeaponFire { get; set; }
    public WeaponReload WeaponReload { get; set; }
    public WeaponAim WeaponAiming { get; set; }
    public Animator WeaponAnimator { get; set; }
    public WeaponMiss WeaponAmmoController { get; set; }
    public RayForShootingProvider RayForShootingProvider { get; set; }
    public WeaponSpawnerAndDestroyer WeaponSpawnerAndDestroyer { get; set; }

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(WeaponActivator).AsSingle();
        Container.BindInstance(WeaponFire).AsSingle();
        Container.BindInstance(WeaponReload).AsSingle();
        Container.BindInstance(WeaponAiming).AsSingle();
        Container.BindInstance(WeaponAnimator).AsSingle();
        Container.BindInstance(WeaponAmmoController).AsSingle();
        Container.BindInstance(RayForShootingProvider).AsSingle();
        Container.BindInstance(WeaponSpawnerAndDestroyer).AsSingle();
    }

    void GetComponents()
    {
        RayForShootingProvider = GetComponent<RayForShootingProvider>();
        WeaponAmmoController = GetComponent<WeaponMiss>();
        WeaponActivator = GetComponent<WeaponActivator>();
        WeaponFire = GetComponent<WeaponFire>();
        WeaponReload = GetComponent<WeaponReload>();
        WeaponAiming = GetComponent<WeaponAim>();
        WeaponAnimator = GetComponent<Animator>();
        WeaponSpawnerAndDestroyer = GetComponent<WeaponSpawnerAndDestroyer>();
    }
}
