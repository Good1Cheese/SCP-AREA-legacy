using UnityEngine;
using Zenject;

public class WearableItemActivator : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    [SerializeField] private Transform _itemParent;

    private ItemActionCreator _itemActionCreator;
    private bool _activatedFromInventory;
    protected PickableInventoryEnablerDisabler _inventoryEnablerDisabler;
    protected WearableItemHandler _wearableItemHandler;
    protected WearableSlot _itemSlot;

    public WearableSlot ItemSlot => _itemSlot;

    [Inject]
    private void Inject(PickableInventoryEnablerDisabler pickableInventoryEnablerDisabler, ItemActionCreator itemActionCreator)
    {
        _inventoryEnablerDisabler = pickableInventoryEnablerDisabler;
        _itemActionCreator = itemActionCreator;
    }

    protected void Start()
    {
        _itemSlot.ItemChanged += SpawnGameObjectForPlayer;
        _itemSlot.ItemRemoved += DeactivateWeapon;
        _itemSlot.Used += ActivateItemFromInventory;
        _inventoryEnablerDisabler.EnabledDisabled += ActivateItemIfActivated;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key)) { return; }

        if (_wearableItemHandler == null || _inventoryEnablerDisabler.IsActivated) { return; }

        SetItemActiveState(!_wearableItemHandler.GameObjectForPlayer.activeSelf);
    }

    public virtual void SetItemActiveState(bool itemActiveState)
    {
        _itemSlot.Toggled?.Invoke(itemActiveState);
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

        Transform itemHandlerTransform = _wearableItemHandler.GameObjectForPlayer.transform;

        itemHandlerTransform.SetParent(_itemParent);
        itemHandlerTransform.localPosition = item_SO.playerGameObjectSpawnOffset;
        itemHandlerTransform.localRotation = Quaternion.identity;

        _wearableItemHandler.GameObjectForPlayer.SetActive(false);
    }

    private void DeactivateWeapon()
    {
        _wearableItemHandler.GameObjectForPlayer.SetActive(false);
        _wearableItemHandler = null;
    }

    private void ActivateItemFromInventory()
    {
        _activatedFromInventory = true;
        _wearableItemHandler.GameObjectForPlayer.SetActive(!_wearableItemHandler.GameObjectForPlayer.activeSelf);
    }

    private void ActivateItemIfActivated()
    {
        if (!_activatedFromInventory) { return; }

        SetItemActiveState(!!_wearableItemHandler.GameObjectForPlayer.activeSelf);
        _activatedFromInventory = false;
    }

    protected void OnDestroy()
    {
        _itemSlot.ItemChanged -= SpawnGameObjectForPlayer;
        _itemSlot.ItemRemoved -= DeactivateWeapon;
        _itemSlot.Used -= ActivateItemFromInventory;
        _inventoryEnablerDisabler.EnabledDisabled -= ActivateItemIfActivated;
    }
}