using UnityEngine;
using Zenject;


public class DoorInteractable : IInteractable
{
    [SerializeField] int m_doorLevel;
    [Inject] readonly EquipmentInventory m_equipmentInventory;

    public override void Interact()
    {
        var keyCard = m_equipmentInventory.KeyCardHandler.Item as KeyCard_SO;
        if (keyCard != null && keyCard.accsesLevel >= m_doorLevel)
        {
            print("Succses");
            return;
        }
        print("Access Denied");
    }
}
    
