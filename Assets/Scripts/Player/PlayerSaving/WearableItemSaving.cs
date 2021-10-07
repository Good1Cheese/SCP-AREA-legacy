using UnityEngine;
using Zenject;

public abstract class WearableItemSaving : DataSaving
{
    [Inject(Id = "PropsHandler")] readonly protected Transform PropsHandler;

    public string itemName;
    public ItemHandler itemHandler;

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(itemName)) { return; }

        GameObject itemGameObject = PropsHandler.Find(itemName).gameObject;
        itemHandler = itemGameObject.GetComponent<ItemHandler>();

        itemHandler.Interact();
    }
}