using Zenject;

public class InjurySlowDownEffect : InjuryEffect
{
    [Inject] private readonly MovementController _movementController;

    protected override float EffectIntensity
    {
        set => _movementController.SlowDownFactor = value;
    }
}