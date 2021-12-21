using UnityEngine;

public abstract class WearableItemHandler : ItemHandler
{
    [SerializeField] protected WearableIte_SO _wearableIte_SO;

    public GameObject GameObjectForPlayer { get; set; }

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

    public override Item_SO Item_SO => _wearableIte_SO;
}