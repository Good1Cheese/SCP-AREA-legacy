using UnityEngine;
using Zenject;

[RequireComponent(typeof(MovementInputLink), typeof(PlayerHealth), typeof(PlayerRotator))]
public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMovement();
        BindHealth();
        BindGameObjectAndTransform();
        BindCamera();

        BindInput();

        Container.BindInstance(GetComponent<CharacterController>())
            .AsSingle();

        Container.BindInstance(GetComponent<RayProvider>())
            .AsSingle();

        Container.BindInstance(GetComponent<PlayerRotator>())
            .AsSingle();
    }

    private void BindMovement()
    {
        Container.Bind<MoveSteps>()
            .FromNew()
            .AsSingle();

        Container.BindInstance(GetComponent<MovementInputLink>())
            .AsSingle();

        Container.BindInstance(GetComponent<SlowWalkEffect>())
            .AsSingle();

        Container.BindInstance(GetComponent<PlayerStamina>())
            .AsSingle();

        Container.BindInstance(GetComponent<StaminaDisabler>())
            .AsSingle();

        Container.BindInstance(GetComponent<MovesContainer>())
            .AsSingle();

        Container.BindInstance(GetComponent<Run>())
            .AsSingle();
        Container.BindInstance(GetComponent<SlowWalkRun>())
            .AsSingle();
        Container.BindInstance(GetComponent<SlowWalk>())
            .AsSingle();

        Container.BindInstance(GetComponent<Walk>())
            .AsSingle();

        Container.BindInstance(GetComponent<StaminaDrain>())
            .AsSingle();
    }

    private void BindHealth()
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

    private void BindGameObjectAndTransform()
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

    private void BindInput()
    {
        Container.BindInstance(GetComponent<InputContainer>())
            .AsSingle();

        Container.BindInstance(GetComponent<MovementInputGetter>())
            .AsSingle();
    }
}