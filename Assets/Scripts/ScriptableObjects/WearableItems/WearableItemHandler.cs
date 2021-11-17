using UnityEngine;
using Zenject;

public abstract class WearableItemHandler : ItemHandler
{
    [Inject] protected readonly WearableItemsInventory _wearableItemsInventory;

    [SerializeField] protected WearableIte_SO _wearableIte_SO;

    public GameObject GameObjectForPlayer { get; set; }

    protected void Awake()
    {
        GameObjectForPlayer = Instantiate(_wearableIte_SO.playerGameObjectPrefab);
        GameObjectForPlayer.SetActive(false);
    }

    public override Ite_SO GetItem()
    {
        return _wearableIte_SO;
    }
}
