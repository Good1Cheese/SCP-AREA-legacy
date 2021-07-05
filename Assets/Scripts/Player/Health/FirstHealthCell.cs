using System.Collections;
using UnityEngine;
using Zenject;

public class FirstHealthCell : HealthCell
{
    [SerializeField] float m_healthIncreasingPerStep;
    [SerializeField] float m_delayWhileHealing;
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly CharacterBleeding m_characterBleeding;

    WaitForSeconds m_timeoutWhileHealing;
    IEnumerator m_playAnimationCoroutine;

    public bool IsHealing { get; set; }

    void Start()
    {
        m_playAnimationCoroutine = PlayAnimationOfHealCoroutine();
        m_timeoutWhileHealing = new WaitForSeconds(m_delayWhileHealing);
    }

    public override void MakeCellEmpty()
    {
        base.MakeCellEmpty();

        if (m_characterBleeding.IsPlayerBleeding) { return; }
        if (IsHealing)
        {
            StopPlayAnimationOfHeal();

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

    public void StopPlayAnimationOfHeal()
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

        StopPlayAnimationOfHeal();
    }

}
