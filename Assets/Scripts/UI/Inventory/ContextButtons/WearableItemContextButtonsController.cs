using UnityEngine;
using Zenject;

[RequireComponent(typeof(IDropable))]
public class WearableItemContextButtonsController : ContextButtonsController
{
    [Inject] readonly new EquipmentInventory PlayerInventory;

    public override void ActiveteOnAction()
    {
        PlayerInventory.OnItemClicked += ActivateContextButtons;
    }

    public override void DeactiveteOnAction()
    {
        PlayerInventory.OnItemClicked -= ActivateContextButtons;
    }
}
