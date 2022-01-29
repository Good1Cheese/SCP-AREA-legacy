using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PlayerRotator))]
public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerMovement();
        BindPlayerHealth();

        BindPlayer();
        BindCamera();

        Container.BindInstance(GetComponent<CharacterController>())
            .AsSingle();

        Container.BindInstance(GetComponent<RayProvider>())
            .AsSingle();

        Container.BindInstance(GetComponent<PlayerRotator>())
            .AsSingle();
    }

    private void BindPlayerMovement()
    {
        Container.BindInstance(GetComponent<PlayerMovement>())
            .AsSingle();
        Container.BindInstance(GetComponent<SlowWalkEffect>())
            .AsSingle();
        Container.BindInstance(GetComponent<PlayerStamina>())
            .AsSingle();
        Container.BindInstance(GetComponent<StaminaDisabler>())
            .AsSingle();
        Container.BindInstance(GetComponent<MovementController>())
            .AsSingle();
        Container.BindInstance(GetComponent<RunController>())
            .AsSingle();
        Container.BindInstance(GetComponent<SlowWalkRunController>())
            .AsSingle();
        Container.BindInstance(GetComponent<SlowWalkController>())
            .AsSingle();
        Container.BindInstance(GetComponent<WalkController>())
            .AsSingle();
        Container.BindInstance(GetComponent<StaminaDrain>())
            .AsSingle();
    }

    private void BindPlayerHealth()
    {
        Container.BindInstance(GetComponent<PlayerHealth>())
            .AsSingle();
        Container.BindInstance(GetComponent<BloodGain>())
            .AsSingle();
        Container.BindInstance(GetComponent<HealableHealth>())
            .AsSingle();
        Container.BindInstance(GetComponent<PlayerBlood>())
            .AsSingle();
        Container.BindInstance(GetComponent<InjuryEffectsController>())
            .AsSingle();
    }

    private void BindPlayer()
    {
        Container.BindInstance(transform)
            .WithId("Player").
            AsCached();

        Container.BindInstance(gameObject)
            .AsSingle();
    }

    private void BindCamera()
    {
        Camera main = Camera.main;

        Container.BindInstance(main)
            .AsCached();

        Container.BindInstance(main.transform)
            .WithId("Camera")
            .AsCached();

        Container.BindInstance(main.GetComponent<GameObjectTrigger>())
            .AsSingle();

        Container.BindInstance(GetComponent<DynamicFov>())
            .AsSingle();
    }
}