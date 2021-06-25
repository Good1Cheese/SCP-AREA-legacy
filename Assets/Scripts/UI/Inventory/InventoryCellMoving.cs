using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class InventoryCellMoving : MonoBehaviour, IDragHandler, IDropHandler
{
    [SerializeField] ImportantVariablesForInventoryCells m_importantVars;

    Vector2 m_startPositon;
    RectTransform m_rectTransform;

    void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_startPositon = m_rectTransform.anchoredPosition;
        enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_rectTransform.anchoredPosition += eventData.delta / m_importantVars.Canvas.scaleFactor / 2;

        Vector2 mousePosition = eventData.position;
        if (mousePosition.x < m_importantVars.XMaxAndMaxPointForDrop.x || mousePosition.x > m_importantVars.XMaxAndMaxPointForDrop.y)
        {

            print("dsa");
            return;
        }

        if (mousePosition.y < m_importantVars.YMinAndMaxPointForDrop.x || mousePosition.y > m_importantVars.YMinAndMaxPointForDrop.y)
        {
            print("dsa");
            return;
        }

        print("***False");

     //   print(eventData.position.y);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item_SO item = eventData.pointerDrag.GetComponent<InventoryCell>().Item;

        // 450 and 1530
        Vector2 mousePosition = eventData.position;
        if (mousePosition.x < m_importantVars.XMaxAndMaxPointForDrop.x || mousePosition.x > m_importantVars.XMaxAndMaxPointForDrop.y)
        {
            DeleteDroppedItem(item);
            print("dsa");
            return;
        }

        // 130 and 775
        if (mousePosition.y < m_importantVars.YMinAndMaxPointForDrop.x || mousePosition.y > m_importantVars.YMinAndMaxPointForDrop.y)
        {
            DeleteDroppedItem(item);
            print("dsa");
            return;
        }

        SetDefaultPosition();
    }

    void DeleteDroppedItem(Item_SO item)
    {
        m_importantVars.m_playerInventory.RemoveItem(item);
        SetDefaultPosition();
    }

    void SetDefaultPosition()
    {
        m_rectTransform.anchoredPosition = m_startPositon;
    }

}


