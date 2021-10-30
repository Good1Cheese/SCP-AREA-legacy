using UnityEngine;
using Zenject;

public abstract class WearableItemSaving : DataSaving
{
    [Inject(Id = "PropsHandler")] readonly protected Transform PropsHandler;

    public string itemName;
    public bool isActive;

    public WearableItemHandler ItemHandler { get; set; }

    protected abstract WearableItemSlot SlotToSave { get; }

    protected virtual void SaveWearableItem() { }

    public override void Save()
    {
        WearableItemSlot slot = SlotToSave;
        ItemHandler = (WearableItemHandler)slot.ItemHandler;

        if (ItemHandler == null) { return; }

        SaveWearableItem();
        isActive = ItemHandler.GameObjectForPlayer.activeSelf;
        itemName = ItemHandler.name;
    }

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(itemName)) { return; }

        GameObject itemGameObject = PropsHandler.Find(itemName).gameObject;
        ItemHandler = itemGameObject.GetComponent<WearableItemHandler>();

        ItemHandler.Interact();
        SlotToSave.WearableItemActivator.SetItemActiveState(isActive);
    }
}