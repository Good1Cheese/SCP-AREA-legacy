using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponActivator), typeof(WeaponRecoil), typeof(WeaponAmmoController))]
public class WeaponInstaller : MonoInstaller
{
    public WeaponActivator WeaponActivator { get; set; }
    public WeaponFire WeaponFire { get; set; }
    public WeaponReload WeaponReload { get; set; }
    public WeaponAim WeaponAiming { get; set; }
    public Animator WeaponAnimator { get; set; }
    public WeaponAmmoController WeaponAmmoController { get; set; }
    public IRayProvider RayForShootingProvider { get; set; }
    public WeaponSpawnerAndDestroyer WeaponGameObjectController { get; set; }

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
        Container.BindInstance(WeaponGameObjectController).AsSingle();
    }

    void GetComponents()
    {
        RayForShootingProvider = GetComponent<IRayProvider>();
        WeaponAmmoController = GetComponent<WeaponAmmoController>();
        WeaponActivator = GetComponent<WeaponActivator>();
        WeaponFire = GetComponent<WeaponFire>();
        WeaponReload = GetComponent<WeaponReload>();
        WeaponAiming = GetComponent<WeaponAim>();
        WeaponAnimator = GetComponent<Animator>();
        WeaponGameObjectController = GetComponent<WeaponSpawnerAndDestroyer>();
    }
}
