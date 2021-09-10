using UnityEngine;

public class PropsHandlerSetter : MonoBehaviour
{
    [SerializeField] WeaponDataSaving m_weaponDataSaving;
    [SerializeField] InventoryDataSaving m_inventoryDataSaving;

    void Awake()
    {
        m_weaponDataSaving.PropsHandler = transform;
        m_inventoryDataSaving.PropsHandler = transform;
    }
}
