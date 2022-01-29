using UnityEngine;
using Zenject;

public abstract class DoorInteractable : Interactable
{
    private KeyCardSlot _keyCardSlot;

    public abstract int KeyCardType { get; }
    public abstract int KeyCardLevelToOpen { get; }

    [Inject]
    private void Inject(KeyCardSlot keyCardSlot)
    {
        _keyCardSlot = keyCardSlot;
    }

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