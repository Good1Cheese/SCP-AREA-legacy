using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerHealth), typeof(PickableItemsInventory))]
public class PlayerInstaller : MonoInstaller
{
    public PlayerMovement PlayerMovement { get; set; }
    public PlayerRotator PlayerRotator { get; set; }
    public PlayerStamina PlayerStamina { get; set; }
    public StaminaUseDisabler StaminaUseDisabler { get; set; }
    public MovementSpeed PlayerSpeed { get; set; }
    public PlayerHealth PlayerHealth { get; set; }
    public RayProvider RayProvider { get; set; }
    public PickableItemsInventory PlayerInventory { get; set; }
    public WearableItemsInventory EquipmentInventory { get; set; }
    public WearableInventoryActivator InventoryAcviteStateSetter { get; set; }
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
        Container.BindInstance(PlayerSpeed).AsSingle();
        Container.BindInstance(PlayerHealth).AsSingle();
        Container.BindInstance(CharacterBleeding).AsSingle();
        Container.BindInstance(PlayerInventory).AsSingle();
        Container.BindInstance(EquipmentInventory).AsSingle();
        Container.BindInstance(InventoryAcviteStateSetter).AsSingle();
        Container.BindInstance(RayProvider).AsSingle();
        Container.BindInstance(this).AsSingle();
        Container.BindInstance(PlayerTransform).AsSingle();
        Container.BindInstance(PlayerGameObject).AsSingle();
    }

    void GetComponents()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerRotator = GetComponent<PlayerRotator>();
        PlayerStamina = GetComponent<PlayerStamina>();
        StaminaUseDisabler = GetComponent<StaminaUseDisabler>();
        PlayerSpeed = GetComponent<MovementSpeed>();
        PlayerHealth = GetComponent<PlayerHealth>();
        CharacterBleeding = GetComponent<CharacterBleeding>();
        PlayerInventory = GetComponent<PickableItemsInventory>();
        EquipmentInventory = GetComponent<WearableItemsInventory>();
        InventoryAcviteStateSetter = GetComponent<WearableInventoryActivator>();
        RayProvider = GetComponent<RayProvider>();
        PlayerTransform = transform;
        PlayerGameObject = gameObject;
    }
}