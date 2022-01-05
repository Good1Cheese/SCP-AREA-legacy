using Zenject;

public class InjurySlowDownEffect : InjuryEffect
{
    [Inject] private readonly MovementController _movementController;

    protected override float EffectValue
    {
        set => _movementController.SlowDownFactor = value;
    }
}