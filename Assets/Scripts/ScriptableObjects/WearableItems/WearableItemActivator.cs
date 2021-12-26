using UnityEngine;
using Zenject;

public class WearableItemActivator : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    [SerializeField] Transform _itemParent;

    [Inject] protected readonly InventoryEnablerDisabler _inventoryEnablerDisabler;
    [Inject] private readonly ItemActionCreator _itemActionCreator;

    protected WearableItemHandler _wearableItemHandler;

    public virtual WearableSlot Slot { get; }

    protected void Start()
    {
        Slot.ItemChanged += SpawnGameObjectForPlayer;
        Slot.ItemRemoved += DeactivateWeapon;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key)) { return; }

        if (_wearableItemHandler == null || _inventoryEnablerDisabler.IsActivated) { return; }

        SetItemActiveState(!_wearableItemHandler.GameObjectForPlayer.activeSelf);
    }

    public virtual void SetItemActiveState(bool itemActiveState)
    {
        Slot.Toggled?.Invoke(itemActiveState);
        SetWearableItemActiveState(itemActiveState);
    }

    protected void SetWearableItemActiveState(bool itemActiveState)
    {
        _wearableItemHandler.GameObjectForPlayer.SetActive(itemActiveState);
        WearableSlot.CurrentItemActivator = this;

        if (itemActiveState) { return; }

        _itemActionCreator.StartEmptyItemActionWithAudioStop();
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

    protected void OnDestroy()
    {
        Slot.ItemChanged -= SpawnGameObjectForPlayer;
        Slot.ItemRemoved -= DeactivateWeapon;
    }
}