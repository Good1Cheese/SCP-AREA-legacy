using UnityEngine;
using Zenject;

public abstract class InjuryEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _intensityCurve;

    private InjuryEffectsController _injuryEffectsController;

    protected abstract float EffectValue { set; }

    [Inject]
    private void Construct(InjuryEffectsController injuryEffectsController)
    {
        _injuryEffectsController = injuryEffectsController;
    }

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