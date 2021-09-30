using Zenject;

public abstract class WearableItemHandler : ItemHandler
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
}
