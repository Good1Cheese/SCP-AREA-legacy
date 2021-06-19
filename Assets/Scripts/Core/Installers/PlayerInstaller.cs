using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(CharacterBleeding))]
public class PlayerInstaller : MonoInstaller
{
    PlayerStamina m_playerStamina;
    PlayerSpeed m_playerSpeed;
    PlayerHealth m_playerHealth;
    CharacterBleeding m_playerBleeding;

    public override void InstallBindings()
    {
        GetComponents();
        Container.Bind<EventManager>().AsSingle().NonLazy();
        Container.BindInstance(m_playerStamina).AsSingle();
        Container.BindInstance(m_playerSpeed).AsSingle();
        Container.BindInstance(m_playerHealth).AsSingle();
        Container.BindInstance(m_playerBleeding).AsSingle();
    }

    void GetComponents()
    {
        m_playerStamina = GetComponent<PlayerStamina>();
        m_playerSpeed = GetComponent<PlayerSpeed>();
        m_playerHealth = GetComponent<PlayerHealth>();
        m_playerBleeding = GetComponent<CharacterBleeding>();
    }
}