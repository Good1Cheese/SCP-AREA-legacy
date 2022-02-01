using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

[RequireComponent(typeof(PickableItemsInventory), typeof(PauseMenuEnablerDisabler))]
public class GameInstaller : MonoInstaller
{
    [SerializeField] private KeyCardSlot _keyCardSlot;
    [SerializeField] private MaskSlot _maskSlot;
    [SerializeField] private UtilitySlot _utilitySlot;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private InjectorSlot _injectorSlot;
    [SerializeField] private Transform _propsHandler;
    [SerializeField] private Transform ActivatorsParent;

    public override void InstallBindings()
    {
        if (_propsHandler == null)
        {
            Debug.LogError("Props Hadnler Field ist's serialized");
        }

        SetItemActivatorsOnSlots();
        BindWearableSlots();
        BindPickableItemsInventory();
        BindRequestsHandlers();

        Container.BindInstance(GetComponent<PauseMenuEnablerDisabler>())
            .AsSingle();

        Container.BindInstance(GetComponent<AmmoUIEnablerDisabler>())
            .AsSingle();

        Container.BindInstance(GetComponent<GameLoader>())
            .AsSingle();

        Container.BindInstance(_propsHandler)
            .WithId("PropsHandler")
            .AsCached();

        Container.BindInstance(GetComponent<Volume>())
            .AsSingle();
    }
    private void SetItemActivatorsOnSlots()
    {
        _keyCardSlot.Activator = ActivatorsParent.GetComponent<KeyCardActivator>();
        _maskSlot.Activator = ActivatorsParent.GetComponent<MaskActivator>();
        _utilitySlot.Activator = ActivatorsParent.GetComponent<UtilityActivator>();
        _weaponSlot.Activator = ActivatorsParent.GetComponent<WeaponActivator>();
        _injectorSlot.Activator = ActivatorsParent.GetComponent<InjectorActivator>();
    }

    private void BindWearableSlots()
    {
        Container.BindInstance(_keyCardSlot)
            .AsSingle();

        Container.BindInstance(_maskSlot)
            .AsSingle();

        Container.BindInstance(_utilitySlot)
            .AsSingle();

        Container.BindInstance(_weaponSlot)
            .AsSingle();

        Container.BindInstance(_injectorSlot)
            .AsSingle();
    }

    private void BindPickableItemsInventory()
    {
        Container.BindInstance(GetComponent<PickableItemsInventory>())
            .AsSingle();

        Container.BindInstance(GetComponent<PickableInventoryEnablerDisabler>())
            .AsSingle();
    }

    private void BindRequestsHandlers()
    {
        Container.BindInstance(GetComponent<InteractableRequestsHandler>())
            .AsSingle();

        Container.BindInstance(GetComponent<WeaponRequestsHandler>())
            .AsSingle();
    }
}