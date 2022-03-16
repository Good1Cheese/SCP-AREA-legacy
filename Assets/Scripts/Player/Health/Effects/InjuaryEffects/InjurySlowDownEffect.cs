using Zenject;

public class InjurySlowDownEffect : InjuryEffect
{
    [Inject] private readonly MovesContainer _movesContainer;

    protected override float EffectValue
    {
        set => _movesContainer.SlowDownFactor = value;
    }
}