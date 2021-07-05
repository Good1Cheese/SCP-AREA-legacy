using UnityEngine;
using Zenject;

[RequireComponent(typeof(IDropable))]
public class WearableItemContextButtonsController : ContextButtonsController
{
    [Inject] readonly EquipmentInventory equipmentInventory;

    public override void ActiveteOnAction()
    {
        equipmentInventory.OnItemClicked += ActivateContextButtons;
    }

    public override void DeactiveteOnAction()
    {
        equipmentInventory.OnItemClicked -= ActivateContextButtons;
    }
}
