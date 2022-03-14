using Zenject;

public class InjurySlowDownEffect : InjuryEffect
{
    [Inject] private readonly MoveSpeed _moveSpeed;

    protected override float EffectValue
    {
        set => _moveSpeed.SlowDownFactor = value;
    }
}