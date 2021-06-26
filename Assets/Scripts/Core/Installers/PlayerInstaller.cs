using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealthSystem), typeof(PlayerInventory))]
public class PlayerInstaller : MonoInstaller
{
    PlayerStamina m_playerStamina;
    MovementSpeed m_playerSpeed;
    PlayerHealthSystem m_playerHealth;
    PlayerInventory m_playerInventory;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_playerStamina).AsSingle();
        Container.BindInstance(m_playerSpeed).AsSingle();
        Container.BindInstance(m_playerHealth).AsSingle();
        Container.BindInstance(m_playerInventory).AsSingle();
        // Container.BindInstances(m_playerStamina, m_playerSpeed, m_playerHealth, m_playerInventory);
    }

    void GetComponents()
    {
        m_playerStamina = GetComponent<PlayerStamina>();
        m_playerSpeed = GetComponent<MovementSpeed>();
        m_playerHealth = GetComponent<PlayerHealthSystem>();
        m_playerInventory = GetComponent<PlayerInventory>();
    }
}