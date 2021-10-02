using UnityEngine.Rendering;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PickableItemsInventory), typeof(PauseMenuEnablerDisabler), typeof(InjuryEffectsController))]
public class GameControllerInstaller : MonoInstaller
{
    PauseMenuEnablerDisabler m_pauseMenuEnablerDisabler;
    InventoryEnablerDisabler m_inventoryEnablerDisabler;
    InjuryEffectsController m_injuryState;
    Volume m_volume;

    public PickableItemsInventory PickableItemsInventory { get; set; }
    public WearableItemsInventory WearableItemsInventory { get; set; }

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(WearableItemsInventory).AsSingle();
        Container.BindInstance(PickableItemsInventory).AsSingle();
        Container.BindInstance(m_pauseMenuEnablerDisabler).AsSingle();
        Container.BindInstance(m_inventoryEnablerDisabler).AsSingle();
        Container.BindInstance(m_injuryState).AsSingle();
        Container.BindInstance(m_volume).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    void GetComponents()
    {
        WearableItemsInventory = GetComponent<WearableItemsInventory>();
        PickableItemsInventory = GetComponent<PickableItemsInventory>();
        m_pauseMenuEnablerDisabler = GetComponent<PauseMenuEnablerDisabler>();
        m_inventoryEnablerDisabler = GetComponent<InventoryEnablerDisabler>();
        m_injuryState = GetComponent<InjuryEffectsController>();
        m_volume = GetComponent<Volume>();
    }
}