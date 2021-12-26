using UnityEngine;
using Zenject;

public abstract class ItemScriptBase : MonoBehaviour
{
    [Inject] protected readonly InventoryEnablerDisabler _inventoryEnablerDisabler;

    protected abstract WearableSlot ItemSlot { get; }
    protected abstract WearableItemHandler WearableItemHandler { get; }

    protected void Start()
    {
        ItemSlot.Toggled += SetActiveState;
        enabled = false;
    }

    private void SetActiveState(bool activeState)
    {
        enabled = activeState;
    }

    protected void OnDestroy()
    {
        ItemSlot.Toggled -= SetActiveState;
    }
}