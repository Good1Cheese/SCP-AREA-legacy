using UnityEngine;
using Zenject;

public class WearableItemActivator : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    [SerializeField] Transform _itemParent;

    [Inject] protected readonly WeaponSlot _wearableItemsInventory;
    [Inject] private readonly InventoryEnablerDisabler _inventoryAcviteStateSetter;

    protected WearableItemHandler _wearableItemHandler;

    public virtual WearableSlot WearableItemSlot { get; }

    protected void Start()
    {
        WearableItemSlot.ItemChanged += SpawnGameObjectForPlayer;
        WearableItemSlot.ItemRemoved += DeactivateWeapon;
        _inventoryAcviteStateSetter.ActiveStateChanged += SetActiveState;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key) || _wearableItemHandler == null) { return; }

        SetItemActiveState(!_wearableItemHandler.GameObjectForPlayer.activeSelf);
        WearableSlot.CurrentItemActivator = this;
    }

    public virtual void SetItemActiveState(bool itemActiveState)
    {
        WearableItemSlot.ItemActionMaker.StartEmptyItemActionWithAudioStop();
        _wearableItemHandler.GameObjectForPlayer.SetActive(itemActiveState);
        WearableItemSlot.Toggled?.Invoke(itemActiveState);
    }

    protected void SpawnGameObjectForPlayer(WearableItemHandler wearableItemHandler)
    {
        _wearableItemHandler = wearableItemHandler;
        WearableIte_SO item_SO = (WearableIte_SO)_wearableItemHandler.Item_SO;

        _wearableItemHandler.GameObjectForPlayer.transform.SetParent(_itemParent);
        _wearableItemHandler.GameObjectForPlayer.transform.localPosition = item_SO.playerGameObjectSpawnOffset;
        _wearableItemHandler.GameObjectForPlayer.transform.localRotation = Quaternion.identity;

        _wearableItemHandler.GameObjectForPlayer.SetActive(false);
    }

    private void DeactivateWeapon()
    {
        _wearableItemHandler.GameObjectForPlayer.SetActive(false);
        _wearableItemHandler = null;
    }

    private void SetActiveState()
    {
        enabled = !enabled;
    }

    protected void OnDestroy()
    {
        WearableItemSlot.ItemChanged -= SpawnGameObjectForPlayer;
        WearableItemSlot.ItemRemoved -= DeactivateWeapon;
        _inventoryAcviteStateSetter.ActiveStateChanged -= SetActiveState;
    }
}