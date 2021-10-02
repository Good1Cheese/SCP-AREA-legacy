using System.Collections;
using UnityEngine;
using Zenject;

public class HealthCellHealEffect : MonoBehaviour
{
    [SerializeField] float m_healthIncreasingPerStep;
    [SerializeField] float m_delayWhileHealing;
    [SerializeField] float m_delayBeforeHealing;

    [Inject] readonly PlayerHealth m_playerHealth;

    WaitForSeconds m_timeoutWhileHealing;
    WaitForSeconds m_timeoutBeforeHealing;
    IEnumerator m_playAnimationCoroutine;

    public bool IsHealContinueable { get; set; }
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
        IsHealContinueable = Cell.Slider.value > 0 && Cell.Slider.value != 1;
        StopCoroutine(m_playAnimationCoroutine);
        m_playAnimationCoroutine = PlayHealEffectCoroutine();
    }

    IEnumerator PlayHealEffectCoroutine()
    {
        yield return m_timeoutBeforeHealing;

        IsHealContinueable = true;

        while (Cell.Slider.maxValue > Cell.Slider.value)
        {
            Cell.Slider.value += m_healthIncreasingPerStep;
            yield return m_timeoutWhileHealing;
        }

        IsHealContinueable = false;

        m_playerHealth.OnPlayerHeals?.Invoke();
        StopHealEffect();
    }
}
