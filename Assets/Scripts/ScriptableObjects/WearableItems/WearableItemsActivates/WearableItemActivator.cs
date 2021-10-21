using UnityEngine;
using Zenject;

public class WearableItemActivator : MonoBehaviour
{
    [SerializeField] KeyCode m_key;

    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

    protected WearableItemHandler m_wearableItemHandler;
    protected WearableItemSlot m_wearableItemSlot;

    void Start()
    {
        m_wearableItemSlot.OnItemChanged += SetItem;
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key) || m_wearableItemHandler == null) { return; }

        SetItemActiveState(!m_wearableItemHandler.WearableItemForPlayer.activeSelf);
    }

    public virtual void SetItemActiveState(bool itemActiveState)
    {
        m_wearableItemHandler.WearableItemForPlayer.SetActive(itemActiveState);
    }

    protected void SetItem(WearableItemHandler wearableItemHandler)
    {
        m_wearableItemHandler = wearableItemHandler;

        m_wearableItemHandler.WearableItemForPlayer.transform.SetParent(transform);
        m_wearableItemHandler.WearableItemForPlayer.transform.localPosition = Vector3.zero;
        m_wearableItemHandler.WearableItemForPlayer.transform.localRotation = Quaternion.identity;

        m_wearableItemHandler.WearableItemForPlayer.SetActive(false);
    }

    protected void OnDestroy()
    {
        m_wearableItemSlot.OnItemChanged -= SetItem;
    }
}