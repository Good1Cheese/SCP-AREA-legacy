using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

public class InjuryLensDistortionEffect : InjuryEffect
{
    LensDistortion m_lensDistortion;

    [Inject]
    void Construct(Volume volume)
    {
        volume.profile.TryGet(out m_lensDistortion);
    }

    protected override float EffectIntensity
    {
        set => m_lensDistortion.intensity.value = value;
    }
}
