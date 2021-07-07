using UnityEngine;

public class InteractionForWearableItemCell : MonoBehaviour, IDropable
{
    ContextButtonsController contextButtonsController;

    PlayerInventory m_playerInventory;

    void Start()
    {
        contextButtonsController = GetComponent<ContextButtonsController>();
        m_playerInventory = contextButtonsController.PlayerInventory;
    }

    public void DropItem()
    {
        m_playerInventory.SpawnItem(contextButtonsController.CurrentCell.Item);
        contextButtonsController.CurrentCell.ClearSlot();
        contextButtonsController.GameObject.SetActive(false);
    }
}
