using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthHeal : MonoBehaviour
{
    [SerializeField] float m_healthIncreasingPerStep;
    [SerializeField] float m_delayWhileHealing;
    [Inject] readonly PlayerHealth m_playerHealth;

    Slider m_slider;
    WaitForSeconds m_timeoutWhileHealing;
    IEnumerator m_playAnimationCoroutine;

    public bool IsHealing { get; set; }

    void Start()
    {
        m_playerHealth.OnFirstInjuary += HealFirstCell;
        m_slider = m_playerHealth.GetFirstHealthCell().Slider;
        m_playAnimationCoroutine = PlayAnimationOfHealCoroutine();
        m_timeoutWhileHealing = new WaitForSeconds(m_delayWhileHealing);
    }

    public void HealFirstCell()
    {
        m_slider.value = 0;

        if (IsHealing)
        {
            StopAPlayAnimationOfHeal();

            m_playerHealth.CurrentHealthCellIndex--;
            m_playerHealth.GetCurrentHealthCell().MakeCellEmpty();
            return;
        }

        PlayAnimationOfHeal();
        m_playerHealth.CurrentHealthCellIndex++;
    }

    public void PlayAnimationOfHeal()
    {
        IsHealing = true;
        StartCoroutine(m_playAnimationCoroutine);
    }

    public void StopAPlayAnimationOfHeal()
    {
        IsHealing = false;
        StopCoroutine(m_playAnimationCoroutine);
        m_playAnimationCoroutine = PlayAnimationOfHealCoroutine();
    }

    IEnumerator PlayAnimationOfHealCoroutine()
    {
        while (m_slider.maxValue > m_slider.value)
        {
            m_slider.value += m_healthIncreasingPerStep;
            yield return m_timeoutWhileHealing;
        }

        StopAPlayAnimationOfHeal();
    }

    void OnDestroy()
    {
        m_playerHealth.OnFirstInjuary -= HealFirstCell;
    }
}
