using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PlayerInventory))]
public class PlayerInstaller : MonoInstaller
{
    public PlayerStamina PlayerStamina { get; set; }
    public MovementSpeed PlayerSpeed { get; set; }
    public PlayerHealth PlayerHealth { get; set; }
    public PlayerInventory PlayerInventory { get; set; }
    public EquipmentInventory EquipmentInventory { get; set; }
    public CharacterBleeding CharacterBleeding { get; set; }

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(PlayerStamina).AsSingle();
        Container.BindInstance(PlayerSpeed).AsSingle();
        Container.BindInstance(PlayerHealth).AsSingle();
        Container.BindInstance(CharacterBleeding).AsSingle();
        Container.BindInstance(PlayerInventory).AsSingle();
        Container.BindInstance(EquipmentInventory).AsSingle();
        Container.BindInstance(this).AsSingle();
        // Container.BindInstances(m_playerStamina, m_playerSpeed, m_playerHealth, m_playerInventory);
    }

    void GetComponents()
    {
        PlayerStamina = GetComponent<PlayerStamina>();
        PlayerSpeed = GetComponent<MovementSpeed>();
        PlayerHealth = GetComponent<PlayerHealth>();
        CharacterBleeding = GetComponent<CharacterBleeding>();
        PlayerInventory = GetComponent<PlayerInventory>();
        EquipmentInventory = GetComponent<EquipmentInventory>();
    }
}