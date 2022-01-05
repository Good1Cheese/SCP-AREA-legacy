using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

[RequireComponent(typeof(PickableItemsInventory), typeof(PauseMenuEnablerDisabler))]
public class GameControllerInstaller : MonoInstaller
{
    [SerializeField] private KeyCardSlot _keyCardSlot;
    [SerializeField] private MaskSlot _maskSlot;
    [SerializeField] private UtilitySlot _utilitySlot;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private InjectorSlot _injectorSlot;
    [SerializeField] private Transform _propsHandler;
    [SerializeField] private Transform ActivatorsParent;

    private ItemActionCreator _itemActionCreator;
    private GameLoader _gameLoader;
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;
    private InventoryEnablerDisabler _inventoryEnablerDisabler;
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
        SetItemActivatorsOnSlots();

        Container.BindInstance(_propsHandler).WithId("PropsHandler").AsCached();
        Container.BindInstance(_itemActionCreator).AsSingle();
        Container.BindInstance(_gameLoader).AsSingle();
        Container.BindInstance(_pauseMenuEnablerDisabler).AsSingle();
        Container.BindInstance(_ammoUIEnablerDisabler).AsSingle();
        Container.BindInstance(_inventoryEnablerDisabler).AsSingle();
        Container.BindInstance(_volume).AsSingle();
        Container.BindInstance(_pickableItemsInventory).AsSingle();
        Container.BindInstance(_wearableItemsInventory).AsSingle();
        Container.BindInstance(_keyCardSlot).AsSingle();
        Container.BindInstance(_maskSlot).AsSingle();
        Container.BindInstance(_utilitySlot).AsSingle();
        Container.BindInstance(_weaponSlot).AsSingle();
        Container.BindInstance(_injectorSlot).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    private void GetComponents()
    {
        _itemActionCreator = GetComponent<ItemActionCreator>();
        _gameLoader = GetComponent<GameLoader>();
        _pauseMenuEnablerDisabler = GetComponent<PauseMenuEnablerDisabler>();
        _ammoUIEnablerDisabler = GetComponent<AmmoUIEnablerDisabler>();
        _inventoryEnablerDisabler = GetComponent<InventoryEnablerDisabler>();
        _volume = GetComponent<Volume>();
        _pickableItemsInventory = GetComponent<PickableItemsInventory>();
        _wearableItemsInventory = GetComponent<WearableItemsInventory>();
    }

    private void SetItemActivatorsOnSlots()
    {
        _keyCardSlot.Activator = ActivatorsParent.GetComponent<KeyCardActivator>();
        _maskSlot.Activator = ActivatorsParent.GetComponent<MaskActivator>();
        _utilitySlot.Activator = ActivatorsParent.GetComponent<UtilityActivator>();
        _weaponSlot.Activator = ActivatorsParent.GetComponent<WeaponActivator>();
        _injectorSlot.Activator = ActivatorsParent.GetComponent<InjectorActivator>();
    }
}