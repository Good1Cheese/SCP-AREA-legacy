using UnityEngine;
using Zenject;

public class WearableItemActivator : InteractableWithDelay
{
    [SerializeField] private KeyCode _key;
    [SerializeField] private Transform _itemParent;

    private bool _activatedFromInventory;
    protected PickableInventoryToggler _inventoryEnablerDisabler;
    protected WearableItemHandler _wearableItemHandler;
    protected WearableSlot _itemSlot;
    private bool _itemActiveState;

    public WearableSlot ItemSlot => _itemSlot;
    public bool IsActive => _wearableItemHandler.GameObjectForPlayer.activeSelf;
    public virtual bool CanItemActivateDeactivate => _wearableItemHandler != null && !_inventoryEnablerDisabler.IsToggled;

    [Inject]
    private void Inject(PickableInventoryToggler pickableInventoryEnablerDisabler)
    {
        _inventoryEnablerDisabler = pickableInventoryEnablerDisabler;
    }

    protected void Start()
    {
        _itemSlot.ItemChanged += SpawnGameObjectForPlayer;
        _itemSlot.ItemRemoved += DeactivateWeapon;
        _itemSlot.Used += ActivateItemFromInventory;
        _inventoryEnablerDisabler.Toggled += ActivateItemIfActivated;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key)) { return; }

        if (CanItemActivateDeactivate)
        {
            TrySetItemActiveState(!IsActive);
        }
    }

    public void TrySetItemActiveState(bool itemActiveState)
    {
        _itemActiveState = itemActiveState;
        TryInteract();
    }

    public void SetItemActiveState(bool itemActiveState)
    {
        _itemActiveState = itemActiveState;
        Interact();
    }

    private void SetItemActiveState()
    {
        _itemSlot.Toggled?.Invoke(_itemActiveState);
        SetWearableItemActiveState();
    }

    protected void SetWearableItemActiveState()
    {
        _wearableItemHandler.GameObjectForPlayer.SetActive(_itemActiveState);
        WearableSlot.CurrentItemActivator = this;
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
        _wearableItemHandler.GameObjectForPlayer.SetActive(!IsActive);
    }

    private void ActivateItemIfActivated()
    {
        if (!_activatedFromInventory) { return; }

        SetItemActiveState(!!IsActive);
        _activatedFromInventory = false;
    }

    public override void Interact() => SetItemActiveState();

    protected void OnDestroy()
    {
        _itemSlot.ItemChanged -= SpawnGameObjectForPlayer;
        _itemSlot.ItemRemoved -= DeactivateWeapon;
        _itemSlot.Used -= ActivateItemFromInventory;
        _inventoryEnablerDisabler.Toggled -= ActivateItemIfActivated;
    }
}