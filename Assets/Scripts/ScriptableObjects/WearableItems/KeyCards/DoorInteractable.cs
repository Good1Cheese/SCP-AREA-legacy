using UnityEngine;
using Zenject;

public abstract class DoorInteractable : MonoBehaviour, IInteractable
{
    private KeyCardSlot _keyCardSlot;

    public abstract int KeyCardType { get; }
    public abstract int KeyCardLevelToOpen { get; }

    [Inject]
    private void Inject(KeyCardSlot keyCardSlot)
    {
        _keyCardSlot = keyCardSlot;
    }

    public void Interact()
    {
        if (!(_keyCardSlot.ItemHandler is KeyCardHandler keycardHandler)
            || !keycardHandler.GameObjectForPlayer.activeSelf
            || (int)keycardHandler.KeyCard_SO.GetKeyCardType() != KeyCardType) { return; }

        if (keycardHandler.KeyCard_SO.KeyCardLevel >= KeyCardLevelToOpen)
        {
            print("Пропуск");
        }
    }
}