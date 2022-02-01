using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponShot), typeof(WeaponShot))]
public class WeaponInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindWeapon();

        Container.BindInstance(GetComponent<RayForFireProvider>())
            .AsSingle();

        Container.BindInstance(GetComponent<AmmoMixup>())
            .AsSingle();

        Container.BindInstance(GetComponent<AudioSource>())
            .WithId("ItemsAudio").AsCached();

        Container.BindInstance(GetComponent<Animator>())
            .AsSingle();
    }

    private void BindWeapon()
    {
        Container.BindInstance(GetComponent<WeaponFire>())
            .AsSingle();

        Container.BindInstance(GetComponent<WeaponShot>())
            .AsSingle();

        Container.BindInstance(GetComponent<WeaponReload>())
            .AsSingle();

        Container.BindInstance(GetComponent<WeaponAim>())
            .AsSingle();

        Container.BindInstance(GetComponent<WeaponNoAmmo>())
            .AsSingle();
    }
}