using UnityEngine;
using Zenject;

public abstract class InjuryEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _intensityCurve;

    [Inject] private readonly InjuryEffectsController _injuryEffectsController;

    protected abstract float EffectValue { set; }

    private void Start()
    {
        _injuryEffectsController.CurveTimeChanged += SetEffectValue;
    }

    private void SetEffectValue(float time)
    {
        EffectValue = _intensityCurve.Evaluate(time);
    }

    private void OnDestroy()
    {
        _injuryEffectsController.CurveTimeChanged -= SetEffectValue;
    }
}