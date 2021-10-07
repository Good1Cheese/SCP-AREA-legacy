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
    IEnumerator m_healDelayCoroutine;
    IEnumerator m_healCoroutine;

    public bool IsHealContinueable { get; set; }
    public bool IsHealing { get; set; }
    public HealthCell Cell { get; set; }

    void Start()
    {
        m_healDelayCoroutine = PlayHealWithDelayCoroutine();
        m_healCoroutine = PlayHealCoroutine();

        m_timeoutWhileHealing = new WaitForSeconds(m_delayWhileHealing);
        m_timeoutBeforeHealing = new WaitForSeconds(m_delayBeforeHealing);
    }

    public void PlayHealWithDelay()
    {
        IsHealing = true;
        StartCoroutine(m_healDelayCoroutine);
    }

    public void PlayHeal()
    {
        IsHealing = true;
        StartCoroutine(m_healCoroutine);
    }

    public void StopHeal()
    {
        IsHealing = false;
        IsHealContinueable = Cell.Slider.value > 0 && Cell.Slider.value != 1;

        StopCoroutine(m_healDelayCoroutine);
        StopCoroutine(m_healCoroutine);

        m_healDelayCoroutine = PlayHealWithDelayCoroutine();
        m_healCoroutine = PlayHealCoroutine();
    }

    IEnumerator PlayHealWithDelayCoroutine()
    {
        yield return m_timeoutBeforeHealing;
        StartCoroutine(m_healCoroutine);
    }

    public IEnumerator PlayHealCoroutine()
    {
        IsHealContinueable = true;

        while (Cell.Slider.maxValue > Cell.Slider.value)
        {
            Cell.Slider.value += m_healthIncreasingPerStep;
            yield return m_timeoutWhileHealing;
        }

        IsHealContinueable = false;

        m_playerHealth.OnPlayerHeals?.Invoke();
        StopHeal();
    }
}
