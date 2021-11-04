using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PlayerRotator))]
public class PlayerInstaller : MonoInstaller
{
    PlayerMovement m_playerMovement;
    PlayerRotator m_playerRotator;
    PlayerStamina m_playerStamina;
    StaminaUseDisabler m_staminaUseDisabler;
    MovementController m_movementController;
    RunController m_runController;
    SlowWalkController m_slowWalkController;
    WalkController m_walkController;
    PlayerHealth m_playerHealth;
    RayProvider m_rayProvider;
    CharacterBleeding CharacterBleeding;
    Transform m_playerTransform;
    GameObject m_playerGameObject;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_playerMovement).AsSingle();
        Container.BindInstance(m_playerRotator).AsSingle();
        Container.BindInstance(m_playerStamina).AsSingle();
        Container.BindInstance(m_staminaUseDisabler).AsSingle();
        Container.BindInstance(m_movementController).AsSingle();
        Container.BindInstance(m_runController).AsSingle();
        Container.BindInstance(m_slowWalkController).AsSingle();
        Container.BindInstance(m_walkController).AsSingle();
        Container.BindInstance(m_playerHealth).AsSingle();
        Container.BindInstance(CharacterBleeding).AsSingle();
        Container.BindInstance(m_rayProvider).AsSingle();
        Container.BindInstance(this).AsSingle();
        Container.BindInstance(m_playerTransform).WithId("Player").AsCached();
        Container.BindInstance(m_playerGameObject).AsSingle();
    }

    void GetComponents()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerRotator = GetComponent<PlayerRotator>();
        m_playerStamina = GetComponent<PlayerStamina>();
        m_staminaUseDisabler = GetComponent<StaminaUseDisabler>();
        m_movementController = GetComponent<MovementController>();
        m_runController = GetComponent<RunController>();
        m_slowWalkController = GetComponent<SlowWalkController>();
        m_walkController = GetComponent<WalkController>();
        m_playerHealth = GetComponent<PlayerHealth>();
        CharacterBleeding = GetComponent<CharacterBleeding>();
        m_rayProvider = GetComponent<RayProvider>();
        m_playerTransform = transform;
        m_playerGameObject = gameObject;
    }
}