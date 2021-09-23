using UnityEngine;
using Zenject;


public class ScientistDoorInteractable : IInteractable
{
    [SerializeField] ScientistKeyCard_SO.TypesOfScientistCard m_typesOfSecurityCard;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    public override void Interact()
    {
        //var keyCard = m_wearableItemsInventory.KeyCardSlot.ItemHandler as ScientistKeyCard_SO;
        //if (keyCard != null && keyCard.TypeOfKeyCard == m_typesOfSecurityCard)
        //{
        //    print("Succses");
        //    return;
        //}
        //print("Access Denied");
    }
}
