using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

public class InjurySaturationEffect : InjuryEffect
{
    private ColorAdjustments _colorAdjustments;

    [Inject]
    private void Construct(Volume volume)
    {
        volume.profile.TryGet(out _colorAdjustments);
    }

    protected override float EffectValue
    {
        set => _colorAdjustments.saturation.value = value;
    }
}