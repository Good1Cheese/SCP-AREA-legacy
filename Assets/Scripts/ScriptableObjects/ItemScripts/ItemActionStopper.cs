using UnityEngine;
using Zenject;

public class ItemActionStopper : MonoBehaviour
{
    private WearableItemsDrop _wearableItemsDrop;
    private PickableItemsInventory _pickableItemsInventory;
    private ItemActionCreator _itemActionCreator;
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private InteractionProvider _interactionProvider;

    [Inject]
    private void Inject(WearableItemsDrop wearableItemsDrop,
                        PickableItemsInventory pickableItemsInventory,
                        ItemActionCreator itemActionCreator,
                        PauseMenuEnablerDisabler pauseMenuEnablerDisabler)
    {
        _wearableItemsDrop = wearableItemsDrop;
        _pickableItemsInventory = pickableItemsInventory;
        _itemActionCreator = itemActionCreator;
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
    }

    private void Start()
    {
        _interactionProvider = GetComponent<InteractionProvider>();

        _pauseMenuEnablerDisabler.EnabledDisabled += PauseUnpauseSound;
        _interactionProvider.Interacted += _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _wearableItemsDrop.ItemRemoved += _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _pickableItemsInventory.ItemRemoved += _itemActionCreator.StartEmptyItemActionWithAudioStop;
    }

    private void PauseUnpauseSound()
    {
        if (_pauseMenuEnablerDisabler.IsActivated)
        {
            _itemActionCreator.SlotAudio.Pause();
            return;
        }
        _itemActionCreator.SlotAudio.UnPause();
    }

    private void OnDestroy()
    {
        _interactionProvider.Interacted -= _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _wearableItemsDrop.ItemRemoved -= _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _pickableItemsInventory.ItemRemoved -= _itemActionCreator.StartEmptyItemActionWithAudioStop;
    }
}