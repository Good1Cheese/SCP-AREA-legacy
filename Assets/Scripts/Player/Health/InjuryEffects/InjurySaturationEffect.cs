using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

public class InjurySaturationEffect : InjuryEffect
{
    ColorAdjustments m_colorAdjustments;

    [Inject]
    void Construct(Volume volume)
    {
        volume.profile.TryGet(out m_colorAdjustments);
    }

    protected override float EffectIntensity
    {
        set => m_colorAdjustments.saturation.value = value;
    }
}