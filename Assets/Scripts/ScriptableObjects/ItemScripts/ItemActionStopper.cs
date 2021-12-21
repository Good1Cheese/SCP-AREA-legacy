using UnityEngine;
using Zenject;

public class ItemActionStopper : MonoBehaviour
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly ItemActionCreator _itemActionCreator;
    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;

    private InteractionProvider _interactionProvider;

    private void Start()
    {
        _interactionProvider = GetComponent<InteractionProvider>();

        _pauseMenuEnablerDisabler.EnabledDisabled += Da;
        _interactionProvider.Interacted += _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _wearableItemsInteraction.ItemRemoved += _itemActionCreator.StartEmptyItemActionWithAudioStop; 
        _pickableItemsInventory.ItemRemoved += _itemActionCreator.StartEmptyItemActionWithAudioStop; 
    }

    private void Da()
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
        _wearableItemsInteraction.ItemRemoved -= _itemActionCreator.StartEmptyItemActionWithAudioStop; 
        _pickableItemsInventory.ItemRemoved -= _itemActionCreator.StartEmptyItemActionWithAudioStop;
    }
}