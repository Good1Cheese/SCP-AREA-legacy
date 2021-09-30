using UnityEngine;

public class PropsHandlerSetter : MonoBehaviour
{
    [SerializeField] PlayerWeaponSaving m_weaponDataSaving;
    [SerializeField] InventorySaving m_inventoryDataSaving;

    void Awake()
    {
        if (m_weaponDataSaving == null || m_inventoryDataSaving == null) 
        {
            Debug.LogError("Fields aren't serialized", this);
            return;
        }

        m_weaponDataSaving.PropsHandler = transform;
        m_inventoryDataSaving.PropsHandler = transform;
    }
}
