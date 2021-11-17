using UnityEngine;
using Zenject;

public abstract class InjuryEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _effectIntensityCurve;

    [Inject] private readonly InjuryEffectsController _injuryEffectsController;

    protected abstract float EffectIntensity { set; }

    private void Start()
    {
        _injuryEffectsController.OnEffectTimeChanging += SetEffectValue;
    }

    private void SetEffectValue(float time)
    {
        EffectIntensity = _effectIntensityCurve.Evaluate(time);
    }

    private void OnDestroy()
    {
        _injuryEffectsController.OnEffectTimeChanging -= SetEffectValue;
    }
}
