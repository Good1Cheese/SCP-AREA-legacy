using UnityEngine;
using Zenject;

public abstract class WearableItemSaving : DataSaving
{
    [Inject(Id = "PropsHandler")] protected readonly Transform PropsHandler;

    public string itemName;
    public bool isActive;

    public WearableItemHandler ItemHandler { get; set; }

    protected abstract WearableSlot SlotToSave { get; }

    protected virtual void SaveWearableItem()
    {
        isActive = ItemHandler.GameObjectForPlayer.activeSelf;
        itemName = ItemHandler.name;
    }

    public override void Save()
    {
        WearableSlot slot = SlotToSave;
        ItemHandler = (WearableItemHandler)slot.ItemHandler;

        if (ItemHandler == null) { return; }

        SaveWearableItem();
    }

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(itemName)) { return; }

        GameObject itemGameObject = PropsHandler.Find(itemName).gameObject;
        ItemHandler = itemGameObject.GetComponent<WearableItemHandler>();

        ItemHandler.Interact();
        SlotToSave.Activator.SetItemActiveState(isActive);
    }
}