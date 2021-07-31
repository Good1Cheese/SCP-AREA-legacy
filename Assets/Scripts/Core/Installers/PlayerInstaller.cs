using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PlayerInventory))]
public class PlayerInstaller : MonoInstaller
{
    public PlayerMovement PlayerMovement { get; set; }
    public PlayerRotator PlayerRotator { get; set; }
    public PlayerStamina PlayerStamina { get; set; }
    public MovementSpeed PlayerSpeed { get; set; }
    public PlayerHealth PlayerHealth { get; set; }
    public RayProvider RayProvider { get; set; }
    public PlayerInventory PlayerInventory { get; set; }
    public EquipmentInventory EquipmentInventory { get; set; }
    public CharacterBleeding CharacterBleeding { get; set; }
    public Volume Volume { get; set; }

    public override void InstallBindings()
    {
        GetComponents();    
        Container.BindInstance(PlayerMovement).AsSingle();
        Container.BindInstance(PlayerRotator).AsSingle();
        Container.BindInstance(PlayerStamina).AsSingle();
        Container.BindInstance(PlayerSpeed).AsSingle();
        Container.BindInstance(PlayerHealth).AsSingle();
        Container.BindInstance(CharacterBleeding).AsSingle();
        Container.BindInstance(PlayerInventory).AsSingle();
        Container.BindInstance(EquipmentInventory).AsSingle();
        Container.BindInstance(RayProvider).AsSingle();
        Container.BindInstance(Volume).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    void GetComponents()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerRotator = GetComponent<PlayerRotator>();
        PlayerStamina = GetComponent<PlayerStamina>();
        PlayerSpeed = GetComponent<MovementSpeed>();
        PlayerHealth = GetComponent<PlayerHealth>();
        CharacterBleeding = GetComponent<CharacterBleeding>();
        PlayerInventory = GetComponent<PlayerInventory>();
        EquipmentInventory = GetComponent<EquipmentInventory>();
        RayProvider = GetComponent<RayProvider>();
        Volume = GetComponent<Volume>();
    }
}