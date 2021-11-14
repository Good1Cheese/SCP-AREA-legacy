using UnityEngine;
using Zenject;

public abstract class InjuryEffect : MonoBehaviour
{
    [SerializeField] AnimationCurve m_effectCurve;

    [Inject] readonly InjuryEffectsController m_injuryEffectsController;

    protected abstract float EffectIntensity { set; }

    void Start()
    {
        m_injuryEffectsController.OnEffectTimeChanging += SetEffectValue;
    }

    void SetEffectValue(float time)
    {
        EffectIntensity = m_effectCurve.Evaluate(time);
    }

    private void OnDestroy()
    {
        m_injuryEffectsController.OnEffectTimeChanging -= SetEffectValue;
    }
}
