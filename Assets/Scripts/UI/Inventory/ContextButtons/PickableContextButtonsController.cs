using UnityEngine;

[RequireComponent(typeof(IDropable), typeof(IUseable))]
public class PickableContextButtonsController : ContextButtonsController
{
    public override void ActiveteOnAction()
    {
        PlayerInventory.OnItemClicked += ActivateContextButtons;
    }

    public override void DeactiveteOnAction()
    {
        PlayerInventory.OnItemClicked -= ActivateContextButtons;
    }
}
