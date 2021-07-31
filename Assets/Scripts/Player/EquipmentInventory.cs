using System;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    [SerializeField] Vector3 m_itemsOffsetForSpawn;
    [SerializeField] KeyCardCell keyCardCell;
    [SerializeField] WeaponCell weaponCell;
    [SerializeField] MaskCell maskCell;

    public KeyCardCell KeyCardCell { get => keyCardCell; }
    public WeaponCell WeaponCell { get => weaponCell; }
    public MaskCell MaskCell { get => maskCell; }

    public Action<WearableItemSlot> OnItemClicked { get; set; }

}
