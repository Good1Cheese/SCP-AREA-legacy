using UnityEngine;
using Zenject;

public abstract class WearableItemHandler : ItemHandler
{
    [Inject] protected readonly m_wearableItemsInventory m_wearableItemsInventory;

    [SerializeField] protected WearableItem_SO m_wearableItem_SO;

    public GameObject GameObjectForPlayer { get; set; }

    protected void Awake()
    {
        GameObjectForPlayer = Instantiate(m_wearableItem_SO.playerGameObjectPrefab);
        GameObjectForPlayer.SetActive(false);
    }

    public override Item_SO GetItem() => m_wearableItem_SO;
}
