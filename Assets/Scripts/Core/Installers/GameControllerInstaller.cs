using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

[RequireComponent(typeof(PickableItemsInventory), typeof(PauseMenuEnablerDisabler), typeof(InjuryLensDistortionEffect))]
public class GameControllerInstaller : MonoInstaller
{
    [SerializeField] private WearableSlot _keyCardSlot;
    [SerializeField] private WearableSlot _maskSlot;
    [SerializeField] private WearableSlot _utilitySlot;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private InjectorSlot _injectorSlot;
    [SerializeField] private Transform _propsHandler;

    private GameLoader _gameLoader;
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;
    private InventoryEnablerDisabler _inventoryEnablerDisabler;
    private InjuryLensDistortionEffect _injuryState;
    private Volume _volume;
    private PickableItemsInventory _pickableItemsInventory;
    private WearableItemsInventory _wearableItemsInventory;

    public override void InstallBindings()
    {
        if (_propsHandler == null)
        {
            Debug.LogError("Props Hadnler Field ist's serialized");
        }

        GetComponents();
        Container.BindInstance(_propsHandler).WithId("PropsHandler").AsCached();
        Container.BindInstance(_gameLoader).AsSingle();
        Container.BindInstance(_pauseMenuEnablerDisabler).AsSingle();
        Container.BindInstance(_ammoUIEnablerDisabler).AsSingle();
        Container.BindInstance(_inventoryEnablerDisabler).AsSingle();
        Container.BindInstance(_injuryState).AsSingle();
        Container.BindInstance(_volume).AsSingle();
        Container.BindInstance(_pickableItemsInventory).AsSingle();
        Container.BindInstance(_wearableItemsInventory).AsSingle();
        Container.BindInstance(_keyCardSlot).WithId("KeyCardSlot").AsCached();
        Container.BindInstance(_maskSlot).WithId("MaskSlot").AsCached();
        Container.BindInstance(_utilitySlot).WithId("UtilitySlot").AsCached();  
        Container.BindInstance(_weaponSlot).AsSingle();
        Container.BindInstance(_injectorSlot).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    private void GetComponents()
    {
        _gameLoader = GetComponent<GameLoader>();
        _pauseMenuEnablerDisabler = GetComponent<PauseMenuEnablerDisabler>();
        _ammoUIEnablerDisabler = GetComponent<AmmoUIEnablerDisabler>();
        _inventoryEnablerDisabler = GetComponent<InventoryEnablerDisabler>();
        _injuryState = GetComponent<InjuryLensDistortionEffect>();
        _volume = GetComponent<Volume>();
        _pickableItemsInventory = GetComponent<PickableItemsInventory>();
        _wearableItemsInventory = GetComponent<WearableItemsInventory>();
    }
}