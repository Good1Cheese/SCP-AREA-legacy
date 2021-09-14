using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChosshairController : MonoBehaviour
{
    [SerializeField] Image m_crosshairWithWeapon;
    [SerializeField] Image m_crosshairWithWeapon1;
    [Inject] readonly WearableItemsInventory m_equipmentInventory;


}
