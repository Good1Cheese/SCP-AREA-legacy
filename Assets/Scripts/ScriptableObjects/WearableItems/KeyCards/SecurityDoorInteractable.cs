using UnityEngine;
using Zenject;

public class SecurityDoorInteractable : IInteractable
{
    [SerializeField] SecurityKeyCard_SO.TypesOfSecurityCard m_typesOfSecurityCard;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    public override void Interact()
    {
        var keyCard = m_wearableItemsInventory.KeyCardSlot.Item as SecurityKeyCard_SO;
        if (keyCard != null && keyCard.TypeOfKeyCard == m_typesOfSecurityCard)
        {
            print("Succses");
            return;
        }
        print("Access Denied");
    }
}
