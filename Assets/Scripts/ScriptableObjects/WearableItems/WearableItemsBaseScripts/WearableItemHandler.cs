using UnityEngine;

public abstract class WearableItemHandler : ItemHandler
{
    [SerializeField] protected WearableIte_SO _wearableIte_SO;

    protected WearableSlot _wearableSlot;

    public GameObject GameObjectForPlayer { get; set; }
    public override Item_SO Item_SO => _wearableIte_SO;

    protected void Awake()
    {
        GameObjectForPlayer = Instantiate(_wearableIte_SO.playerGameObjectPrefab);
        GameObjectForPlayer.SetActive(false);
    }

    public override void Interact()
    {
        Equiped();
        base.Interact();
    }

    public override void Clicked(int slotIndex)
    {
        _wearableSlot.Used?.Invoke();
    }

    public override void Dropped()
    {
        base.Dropped();
        IsInInventory = false;
    }
}