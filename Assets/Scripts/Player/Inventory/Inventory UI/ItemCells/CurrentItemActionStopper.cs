using UnityEngine;
using Zenject;

public class CurrentItemActionStopper : MonoBehaviour
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly ItemActionCreator _itemActionCreator;

    private InteractionProvider _interactionProvider;

    private void Start()
    {
        _interactionProvider = GetComponent<InteractionProvider>();

        _interactionProvider.Interacted += _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _wearableItemsInteraction.ItemRemoved += _itemActionCreator.StartEmptyItemActionWithAudioStop; 
        _pickableItemsInventory.ItemRemoved += _itemActionCreator.StartEmptyItemActionWithAudioStop; 
    }


    private void OnDestroy()
    {
        _interactionProvider.Interacted -= _itemActionCreator.StartEmptyItemActionWithAudioStop;
        _wearableItemsInteraction.ItemRemoved -= _itemActionCreator.StartEmptyItemActionWithAudioStop; 
        _pickableItemsInventory.ItemRemoved -= _itemActionCreator.StartEmptyItemActionWithAudioStop;
    }
}