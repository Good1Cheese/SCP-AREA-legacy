using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class InventorySlotMoving : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector2 m_startPositon;
    [SerializeField] Canvas m_canvas;
    RectTransform m_rectTransform;

    void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_startPositon = m_rectTransform.anchoredPosition;
        ItemReturner.OnItemDroppedInSafeZone += SetDefaultPosition;
        enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_rectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor / 2;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetDefaultPosition();
    }

    void SetDefaultPosition()
    {
        m_rectTransform.anchoredPosition = m_startPositon;
    }

}
