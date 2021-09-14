using UnityEngine;
using Zenject;


public class ScientistDoorInteractable : IInteractable
{
    [SerializeField] ScientistKeyCard_SO.TypesOfScientistCard m_typesOfSecurityCard;
    [Inject] readonly WearableItemsInventory m_equipmentInventory;

    public override void Interact()
    {
        var keyCard = m_equipmentInventory.KeyCardSlot.Item as ScientistKeyCard_SO;
        if (keyCard != null && keyCard.TypeOfKeyCard == m_typesOfSecurityCard)
        {
            print("Succses");
            return;
        }
        print("Access Denied");
    }
}
