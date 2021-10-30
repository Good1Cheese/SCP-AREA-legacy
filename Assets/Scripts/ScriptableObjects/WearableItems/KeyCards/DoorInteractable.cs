using Zenject;

public abstract class DoorInteractable : IInteractable
{
    public abstract int KeyCardType { get; }
    public abstract int KeyCardLevelToOpen { get; }

    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    public override void Interact()
    {
        var keycardHandler = m_wearableItemsInventory.KeyCardSlot.ItemHandler as KeyCardHandler;

        if (keycardHandler == null  || !keycardHandler.GameObjectForPlayer.activeSelf || (int)keycardHandler.KeyCard_SO.GetKeyCardType() != KeyCardType) { return; }

        if (keycardHandler.KeyCard_SO.KeyCardLevel >= KeyCardLevelToOpen)
        {
            print("Пропуск");
        }
    }
}
