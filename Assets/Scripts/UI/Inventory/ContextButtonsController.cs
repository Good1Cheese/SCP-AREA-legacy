using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ContextButtonsController : MonoBehaviour
{
    [SerializeField] Vector2 m_offset;
    [Inject] PlayerInventory m_playerInventory;

    Item_SO m_currentItem;
    Transform m_transform;
    GameObject m_gameObject;

    void Start()
    {
        m_transform = transform;
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
        InventoryCell.OnItemClicked += ActivateContextButtons;
    }

    public void ActivateContextButtons(PointerEventData position)
    {
        m_currentItem = position.pointerClick.GetComponent<InventoryCell>().Item;
        m_gameObject.SetActive(true);
        m_transform.position = position.position + m_offset;
    }

    public void UseItem()
    {
        m_currentItem.Use();
        m_gameObject.SetActive(false);
    }

    public void DeleteItem()
    {
        m_playerInventory.RemoveItem(m_currentItem);
        m_gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        InventoryCell.OnItemClicked -= ActivateContextButtons;
    }
}
