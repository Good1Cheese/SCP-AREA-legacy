using UnityEngine;

public class InteractionForPickableItemCell : MonoBehaviour, IUseable, IDropable
{
    ContextButtonsController contextButtonsController;

    PlayerInventory m_playerInventory;

    void Start()
    {
        contextButtonsController = GetComponent<ContextButtonsController>();
        m_playerInventory = contextButtonsController.PlayerInventory;
    }

    public void UseItem()
    {
        contextButtonsController.CurrentCell.Item.Use();
        m_playerInventory.RemoveItem(contextButtonsController.CurrentCell.Item);
        contextButtonsController.GameObject.SetActive(false);
    }

    public void DropItem()
    {
        m_playerInventory.SpawnItem(contextButtonsController.CurrentCell.Item);
        m_playerInventory.RemoveItem(contextButtonsController.CurrentCell.Item);
        contextButtonsController.GameObject.SetActive(false);
    }

}
