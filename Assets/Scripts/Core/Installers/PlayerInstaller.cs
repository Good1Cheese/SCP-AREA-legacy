using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PlayerRotator))]
public class PlayerInstaller : MonoInstaller
{
    public PlayerMovement PlayerMovement { get; set; }
    public PlayerRotator PlayerRotator { get; set; }
    public PlayerStamina PlayerStamina { get; set; }
    public StaminaUseDisabler StaminaUseDisabler { get; set; }
    public MovementController MovementController { get; set; }
    public RunController RunController { get; set; }
    public SlowWalkController SlowWalkController { get; set; }
    public PlayerHealth PlayerHealth { get; set; }
    public RayProvider RayProvider { get; set; }
    public CharacterBleeding CharacterBleeding { get; set; }
    public Transform PlayerTransform { get; set; }
    public GameObject PlayerGameObject { get; set; }

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(PlayerMovement).AsSingle();
        Container.BindInstance(PlayerRotator).AsSingle();
        Container.BindInstance(PlayerStamina).AsSingle();
        Container.BindInstance(StaminaUseDisabler).AsSingle();
        Container.BindInstance(MovementController).AsSingle();
        Container.BindInstance(RunController).AsSingle();
        Container.BindInstance(SlowWalkController).AsSingle();
        Container.BindInstance(PlayerHealth).AsSingle();
        Container.BindInstance(CharacterBleeding).AsSingle();
        Container.BindInstance(RayProvider).AsSingle();
        Container.BindInstance(this).AsSingle();
        Container.BindInstance(PlayerTransform).WithId("Player").AsCached();
        Container.BindInstance(PlayerGameObject).AsSingle();
    }

    void GetComponents()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerRotator = GetComponent<PlayerRotator>();
        PlayerStamina = GetComponent<PlayerStamina>();
        StaminaUseDisabler = GetComponent<StaminaUseDisabler>();
        MovementController = GetComponent<MovementController>();
        RunController = GetComponent<RunController>();
        SlowWalkController = GetComponent<SlowWalkController>();
        PlayerHealth = GetComponent<PlayerHealth>();
        CharacterBleeding = GetComponent<CharacterBleeding>();
        RayProvider = GetComponent<RayProvider>();
        PlayerTransform = transform;
        PlayerGameObject = gameObject;
    }
}