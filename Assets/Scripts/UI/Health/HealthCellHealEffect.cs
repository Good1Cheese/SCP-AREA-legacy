using System.Collections;
using UnityEngine;

public class HealthCellHealEffect : MonoBehaviour
{
    [SerializeField] float m_healthIncreasingPerStep;
    [SerializeField] float m_delayWhileHealing;
    [SerializeField] float m_delayBeforeHealing;

    WaitForSeconds m_timeoutWhileHealing;
    WaitForSeconds m_timeoutBeforeHealing;
    IEnumerator m_playAnimationCoroutine;

    public bool IsHealing { get; set; }
    public HealthCell Cell { get; set; }

    void Start()
    {
        m_playAnimationCoroutine = PlayHealEffectCoroutine();
        m_timeoutWhileHealing = new WaitForSeconds(m_delayWhileHealing);
        m_timeoutBeforeHealing = new WaitForSeconds(m_delayBeforeHealing);
    }

    public void StartHealEffect()
    {
        IsHealing = true;
        StartCoroutine(m_playAnimationCoroutine);
    }

    public void StopHealEffect()
    {
        IsHealing = false;
        StopCoroutine(m_playAnimationCoroutine);
        m_playAnimationCoroutine = PlayHealEffectCoroutine();
    }

    IEnumerator PlayHealEffectCoroutine()
    {
        yield return m_timeoutBeforeHealing;

        while (Cell.Slider.maxValue > Cell.Slider.value)
        {
            Cell.Slider.value += m_healthIncreasingPerStep;
            yield return m_timeoutWhileHealing;
        }

        StopHealEffect();
    }
}
