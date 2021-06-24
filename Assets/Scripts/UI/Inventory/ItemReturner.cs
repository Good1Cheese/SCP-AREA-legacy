using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemReturner : MonoBehaviour, IDropHandler
{
    public static Action OnItemDroppedInSafeZone { get; set; }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedInSafeZone.Invoke();
    }
}
