using System;
using UnityEngine;

public class WearableItemsInventory : MonoBehaviour
{
    [SerializeField] Vector3 m_itemsOffsetForSpawn;
    [SerializeField] KeyCardSlot keyCardSlot;
    [SerializeField] WeaponSlot weaponSlot;
    [SerializeField] MaskSlot maskSlot;

    public KeyCardSlot KeyCardSlot { get => keyCardSlot; }
    public WeaponSlot WeaponSlot { get => weaponSlot; }
    public MaskSlot MaskSlot { get => maskSlot; }

    public Action<WearableItemSlot> OnItemClicked { get; set; }

}
