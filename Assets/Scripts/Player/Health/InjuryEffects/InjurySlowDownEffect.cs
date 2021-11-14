using Zenject;

public class InjurySlowDownEffect : InjuryEffect
{
    [Inject] readonly MovementController m_movementController;

    protected override float EffectIntensity 
    {
        set => m_movementController.SlowDownFactor = value;
    }
}