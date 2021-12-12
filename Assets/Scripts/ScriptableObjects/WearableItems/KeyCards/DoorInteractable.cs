using Zenject;

public abstract class DoorInteractable : IInteractable
{
    public abstract int KeyCardType { get; }
    public abstract int KeyCardLevelToOpen { get; }

    [Inject] private readonly KeyCardSlot _keyCardSlot;

    public override void Interact()
    {
        KeyCardHandler keycardHandler = _keyCardSlot.ItemHandler as KeyCardHandler;

        if (keycardHandler == null || !keycardHandler.GameObjectForPlayer.activeSelf || (int)keycardHandler.KeyCard_SO.GetKeyCardType() != KeyCardType) { return; }

        if (keycardHandler.KeyCard_SO.KeyCardLevel >= KeyCardLevelToOpen)
        {
            print("Пропуск");
        }
    }
}
