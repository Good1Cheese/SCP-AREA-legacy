using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

[RequireComponent(typeof(PickableItemsInventory), typeof(PauseMenuEnablerDisabler), typeof(InjuryLensDistortionEffect))]
public class GameControllerInstaller : MonoInstaller
{
    [SerializeField] private Transform _propsHandler;
    private GameLoader _gameLoader;
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;
    private InventoryEnablerDisabler _inventoryEnablerDisabler;
    private InjuryLensDistortionEffect _injuryState;
    private Volume _volume;

    public PickableItemsInventory PickableItemsInventory { get; set; }
    public WearableItemsInventory WearableItemsInventory { get; set; }

    public override void InstallBindings()
    {
        if (_propsHandler == null)
        {
            Debug.LogError("Props Hadnler Field ist's serialized");
        }

        GetComponents();
        Container.BindInstance(_propsHandler).WithId("PropsHandler").AsCached();
        Container.BindInstance(WearableItemsInventory).AsSingle();
        Container.BindInstance(PickableItemsInventory).AsSingle();
        Container.BindInstance(_gameLoader).AsSingle();
        Container.BindInstance(_pauseMenuEnablerDisabler).AsSingle();
        Container.BindInstance(_ammoUIEnablerDisabler).AsSingle();
        Container.BindInstance(_inventoryEnablerDisabler).AsSingle();
        Container.BindInstance(_injuryState).AsSingle();
        Container.BindInstance(_volume).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    private void GetComponents()
    {
        WearableItemsInventory = GetComponent<WearableItemsInventory>();
        PickableItemsInventory = GetComponent<PickableItemsInventory>();
        _gameLoader = GetComponent<GameLoader>();
        _pauseMenuEnablerDisabler = GetComponent<PauseMenuEnablerDisabler>();
        _ammoUIEnablerDisabler = GetComponent<AmmoUIEnablerDisabler>();
        _inventoryEnablerDisabler = GetComponent<InventoryEnablerDisabler>();
        _injuryState = GetComponent<InjuryLensDistortionEffect>();
        _volume = GetComponent<Volume>();
    }
}