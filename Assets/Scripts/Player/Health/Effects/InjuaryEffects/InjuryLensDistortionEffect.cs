using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

public class InjuryLensDistortionEffect : InjuryEffect
{
    private LensDistortion _lensDistortion;

    [Inject]
    private void Construct(Volume volume)
    {
        volume.profile.TryGet(out _lensDistortion);
    }

    protected override float EffectIntensity
    {
        set => _lensDistortion.intensity.value = value;
    }
}
