using System;
using UnityEngine;

public class WearableItemsInventory : MonoBehaviour
{
    [SerializeField] KeyCardSlot m_keyCardSlot;
    [SerializeField] WeaponSlot m_weaponSlot;
    [SerializeField] MaskSlot m_maskSlot;

    public KeyCardSlot KeyCardSlot { get => m_keyCardSlot; }
    public WeaponSlot WeaponSlot { get => m_weaponSlot; }
    public MaskSlot MaskSlot { get => m_maskSlot; }

    public Action<WearableItemSlot> OnItemClicked { get; set; }

    void Start()
    {
        if (m_keyCardSlot == null || m_weaponSlot == null || m_maskSlot == null)
        {
            Debug.LogError("Fields aren't serialized", this);
        }    
    }
}
