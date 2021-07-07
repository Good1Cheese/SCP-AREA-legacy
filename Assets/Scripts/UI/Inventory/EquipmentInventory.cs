using System;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public KeyCardCell KeyCardHandler { get; set; }
    public WeaponCell WeaponHandler { get; set; }
    public MaskCell MaskHandler { get; set; }

    public Action<Vector2, InventorySlot> OnItemClicked { get; set; }

}
