using UnityEngine;

public class WearableItemsInventory : MonoBehaviour
{
    [SerializeField] WearableItemSlot m_keyCardSlot;
    [SerializeField] WearableItemSlot m_maskSlot;
    [SerializeField] WeaponSlot m_weaponSlot;
    [SerializeField] WearableItemSlot m_deviceSlot;
    [SerializeField] InjectorSlot m_injectorSlot;

    public WearableItemSlot KeyCardSlot { get => m_keyCardSlot; }
    public WearableItemSlot MaskSlot { get => m_maskSlot; }
    public WeaponSlot WeaponSlot { get => m_weaponSlot; }
    public WearableItemSlot DeviceSlot { get => m_deviceSlot; }
    public InjectorSlot InjectorSlot { get => m_injectorSlot;  }

    void Start()
    {
        if (m_keyCardSlot == null
            || m_weaponSlot == null
            || m_maskSlot == null
            || m_deviceSlot == null
            || InjectorSlot == null)
        {
            Debug.LogError("Fields aren't serialized", this);
        }
    }
}
