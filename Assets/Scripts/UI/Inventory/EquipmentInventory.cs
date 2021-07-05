using System;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public WearableItemSlot KeyCardHandler { get; set; }
    public WearableItemSlot WeaponHandler { get; set; }
    public WearableItemSlot MaskHandler { get; set; }

    public Action<Vector2, InventorySlot> OnItemClicked { get; set; }

}
