using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PlayerRotator))]
public class PlayerInstaller : MonoInstaller
{
    private SlowWalkEffect _slowWalkEffect;
    private DynamicFov _dynamicFov;
    private InjuryEffectsController _injuryEffectsController;
    private PlayerMovement _playerMovement;
    private PlayerRotator _playerRotator;
    private PlayerStamina _playerStamina;
    private StaminaDisabler _staminaUseDisabler;
    private MovementController _movementController;
    private RunController _runController;
    private SlowWalkRunController _slowWalkRunController;
    private SlowWalkController _slowWalkController;
    private WalkController _walkController;
    private PlayerHealth _playerHealth;
    private HealableHealth _healableHealth;
    private RayProvider _rayProvider;
    private PlayerBlood _playerBlood;
    private Transform _playerTransform;
    private GameObject _playerGameObject;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(_slowWalkEffect).AsSingle();
        Container.BindInstance(_dynamicFov).AsSingle();
        Container.BindInstance(_injuryEffectsController).AsSingle();
        Container.BindInstance(_playerMovement).AsSingle();
        Container.BindInstance(_playerRotator).AsSingle();
        Container.BindInstance(_playerStamina).AsSingle();
        Container.BindInstance(_staminaUseDisabler).AsSingle();
        Container.BindInstance(_movementController).AsSingle();
        Container.BindInstance(_runController).AsSingle();
        Container.BindInstance(_slowWalkRunController).AsSingle();
        Container.BindInstance(_slowWalkController).AsSingle();
        Container.BindInstance(_walkController).AsSingle();
        Container.BindInstance(_playerHealth).AsSingle();
        Container.BindInstance(_healableHealth).AsSingle();
        Container.BindInstance(_playerBlood).AsSingle();
        Container.BindInstance(_rayProvider).AsSingle();
        Container.BindInstance(this).AsSingle();
        Container.BindInstance(_playerTransform).WithId("Player").AsCached();
        Container.BindInstance(_playerGameObject).AsSingle();

        Camera main = Camera.main;
        Container.BindInstance(main).AsCached();
        Container.BindInstance(main.GetComponent<GameObjectTrigger>()).AsSingle();
    }

    private void GetComponents()
    {
        _slowWalkEffect = GetComponent<SlowWalkEffect>();
        _dynamicFov = GetComponent<DynamicFov>();
        _injuryEffectsController = GetComponent<InjuryEffectsController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRotator = GetComponent<PlayerRotator>();
        _playerStamina = GetComponent<PlayerStamina>();
        _staminaUseDisabler = GetComponent<StaminaDisabler>();
        _movementController = GetComponent<MovementController>();
        _runController = GetComponent<RunController>();
        _slowWalkRunController = GetComponent<SlowWalkRunController>();
        _slowWalkController = GetComponent<SlowWalkController>();
        _walkController = GetComponent<WalkController>();
        _playerHealth = GetComponent<PlayerHealth>();
        _healableHealth = GetComponent<HealableHealth>();
        _playerBlood = GetComponent<PlayerBlood>();
        _rayProvider = GetComponent<RayProvider>();
        _playerTransform = transform;
        _playerGameObject = gameObject;
    }
}