using System;
using UnityEngine;

public class InjectorSlot : WearableSlot
{
    public Action<InjectorHandler> OnInjectorChanged { get; set; }
    public Action OnSlotUsed { get; set; }

    public override void OnItemSet()
    {
        base.OnItemSet();

        OnInjectorChanged.Invoke(ItemHandler as InjectorHandler);
        OnItemToggled?.Invoke(false);
    }
}