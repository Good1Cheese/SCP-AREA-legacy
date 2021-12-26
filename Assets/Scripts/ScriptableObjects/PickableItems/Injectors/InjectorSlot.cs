using System;

public class InjectorSlot : WearableSlot
{
    public Action<InjectorHandler> Changed { get; set; }
    public Action Used { get; set; }

    public override void Setted()
    {
        base.Setted();

        Changed.Invoke(ItemHandler as InjectorHandler);
        Toggled?.Invoke(false);
    }
}