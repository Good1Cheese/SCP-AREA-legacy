using UnityEngine;
using Zenject;

public class WearableItemActivator : MonoBehaviour
{
    [SerializeField] KeyCode m_key;

    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly InventoryEnablerDisabler m_inventoryAcviteStateSetter;

    protected WearableItemHandler m_wearableItemHandler;
    protected WearableItemSlot m_wearableItemSlot;

    protected void Start()
    {
        m_wearableItemSlot.OnItemChanged += SetItem;
        m_wearableItemSlot.OnItemRemoved += DeactivateWeapon;
        m_inventoryAcviteStateSetter.OnInventoryEnabledDisabled += SetActiveState;
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key) || m_wearableItemHandler == null) { return; }

        SetItemActiveState(!m_wearableItemHandler.GameObjectForPlayer.activeSelf);
    }

    public virtual void SetItemActiveState(bool itemActiveState)
    {
        m_wearableItemHandler.GameObjectForPlayer.SetActive(itemActiveState);
    }

    protected void SetItem(WearableItemHandler wearableItemHandler)
    {
        m_wearableItemHandler = wearableItemHandler;
        var item_SO = (WearableItem_SO)m_wearableItemHandler.GetItem(); 

        m_wearableItemHandler.GameObjectForPlayer.transform.SetParent(transform);
        m_wearableItemHandler.GameObjectForPlayer.transform.localPosition = item_SO.playerGameObjectspawnOffset;
        m_wearableItemHandler.GameObjectForPlayer.transform.localRotation = Quaternion.identity;

        m_wearableItemHandler.GameObjectForPlayer.SetActive(false);
    }

    void DeactivateWeapon()
    {
        m_wearableItemHandler.GameObjectForPlayer.SetActive(false);
        m_wearableItemHandler = null;
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    protected void OnDestroy()
    {
        m_wearableItemSlot.OnItemChanged -= SetItem;
        m_wearableItemSlot.OnItemRemoved -= DeactivateWeapon;
        m_inventoryAcviteStateSetter.OnInventoryEnabledDisabled -= SetActiveState;
    }
}