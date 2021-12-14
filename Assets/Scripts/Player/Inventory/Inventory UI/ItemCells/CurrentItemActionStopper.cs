using UnityEngine;
using Zenject;

public class CurrentItemActionStopper : MonoBehaviour
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    private InteractionProvider _interactionProvider;

    private void Start()
    {
        _interactionProvider = GetComponent<InteractionProvider>();
        _interactionProvider.Interacted += StopCurrentItemAction;
        _pickableItemsInventory.ItemRemoved += StopCurrentItemAction;
    }

    private void StopCurrentItemAction()
    {
        var currentItemActivator = WearableSlot.CurrentItemActivator;
        if (currentItemActivator != null)
        {
            currentItemActivator.WearableItemSlot.ItemActionMaker.StartEmptyItemActionWithAudioStop();
        }
    }

    private void OnDestroy()
    {
        _interactionProvider.Interacted -= StopCurrentItemAction;
        _pickableItemsInventory.ItemRemoved -= StopCurrentItemAction;
    }
}