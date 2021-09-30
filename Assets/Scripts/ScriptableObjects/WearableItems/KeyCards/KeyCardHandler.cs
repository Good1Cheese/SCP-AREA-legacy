﻿using UnityEngine;

public class KeyCardHandler : WearableItemHandler
{
    [SerializeField] KeyCard_SO m_keyCard_SO;

    public KeyCard_SO KeyCard_SO { get => m_keyCard_SO; }

    public override Item_SO GetItem() => m_keyCard_SO;

    public override void Equip()
    {
        m_wearableItemsInventory.KeyCardSlot.SetItem(this);
    }
}