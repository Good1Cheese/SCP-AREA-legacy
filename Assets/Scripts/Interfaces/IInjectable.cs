using UnityEngine;

public interface IInjectable
{
    public int NumOfUses { get; }

    public abstract void Inject();
}

public interface IAdrenalinInjectable : IInjectable
{
}

public interface IHealthInjectable : IInjectable
{
}