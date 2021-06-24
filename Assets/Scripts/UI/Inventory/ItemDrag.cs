using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ItemDrag : MonoBehaviour, IDropHandler
{
    [Inject] PlayerInventory m_playerInventory;

    public void OnDrop(PointerEventData eventData)
    {
        m_playerInventory.RemoveItem(eventData.pointerDrag.GetComponent<InventorySlot>().Item);
    }
}