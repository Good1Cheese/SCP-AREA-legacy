using UnityEngine.Rendering;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PickableItemsInventory), typeof(PauseMenuEnablerDisabler), typeof(InjuryEffectsController))]
public class GameControllerInstaller : MonoInstaller
{
    [SerializeField] Transform m_propsHandler;
    GameLoader m_gameLoader;
    PauseMenuEnablerDisabler m_pauseMenuEnablerDisabler;
    InventoryEnablerDisabler m_inventoryEnablerDisabler;
    InjuryEffectsController m_injuryState;
    Volume m_volume;

    public PickableItemsInventory PickableItemsInventory { get; set; }
    public m_wearableItemsInventory WearableItemsInventory { get; set; }

    public override void InstallBindings()
    {
        if (m_propsHandler == null)
        {
            Debug.LogError("Props Hadnler Field ist's serialized");
        }

        GetComponents();
        Container.BindInstance(m_propsHandler).WithId("PropsHandler").AsCached();
        Container.BindInstance(WearableItemsInventory).AsSingle();
        Container.BindInstance(PickableItemsInventory).AsSingle();
        Container.BindInstance(m_gameLoader).AsSingle();
        Container.BindInstance(m_pauseMenuEnablerDisabler).AsSingle();
        Container.BindInstance(m_inventoryEnablerDisabler).AsSingle();
        Container.BindInstance(m_injuryState).AsSingle();
        Container.BindInstance(m_volume).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    void GetComponents()
    {
        WearableItemsInventory = GetComponent<m_wearableItemsInventory>();
        PickableItemsInventory = GetComponent<PickableItemsInventory>();
        m_gameLoader = GetComponent<GameLoader>();
        m_pauseMenuEnablerDisabler = GetComponent<PauseMenuEnablerDisabler>();
        m_inventoryEnablerDisabler = GetComponent<InventoryEnablerDisabler>();
        m_injuryState = GetComponent<InjuryEffectsController>();
        m_volume = GetComponent<Volume>();
    }
}