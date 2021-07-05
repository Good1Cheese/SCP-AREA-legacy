using UnityEngine;

public class InteractionForPickableItemCell : MonoBehaviour, IUseable, IDropable
{
    ContextButtonsController contextButtonsController;

    public PlayerInventory PlayerInventory { get; set; }

    void Start()
    {
        contextButtonsController = GetComponent<ContextButtonsController>();
        PlayerInventory = contextButtonsController.PlayerInventory;
    }

    public void UseItem()
    {
        contextButtonsController.CurrentCell.Item.Use();
        PlayerInventory.RemoveItem(contextButtonsController.CurrentCell.Item);
        contextButtonsController.GameObject.SetActive(false);
    }

    public void DropItem()
    {
        PlayerInventory.SpawnItem(contextButtonsController.CurrentCell.Item);
        PlayerInventory.RemoveItem(contextButtonsController.CurrentCell.Item);
        contextButtonsController.GameObject.SetActive(false);
    }

}
