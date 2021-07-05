using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(IDropable), typeof(IUseable))]
public class PickableContextButtonsController : ContextButtonsController
{
    [Inject] readonly PlayerInventory playerInventory;

    public override void ActiveteOnAction()
    {
        playerInventory.OnItemClicked += ActivateContextButtons;
    }

    public override void DeactiveteOnAction()
    {
        playerInventory.OnItemClicked -= ActivateContextButtons;
    }
}
