using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthCellHealEffect))]
public class HealableHealthCell : HealthCell
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly CharacterBleeding m_characterBleeding;

    HealthCellHealEffect m_healthCellHealEffect;

    void Start()
    {
        m_healthCellHealEffect = GetComponent<HealthCellHealEffect>();
        m_healthCellHealEffect.Cell = this;
        m_playerHealth.OnPlayerHeals += HealCell;
    }

    void HealCell()
    {
        if (m_playerHealth.CurrentCellIndex + 1 != m_playerHealth.HealableCellIndex) { return; }
        m_healthCellHealEffect.StartHealEffect();
    }

    public override void MakeCellEmpty()
    {
        base.MakeCellEmpty();

        if (m_characterBleeding.IsPlayerBleeding) { return; }

        if (m_healthCellHealEffect.IsHealing)
        {
            m_healthCellHealEffect.StopHealEffect();

            m_playerHealth.CurrentCellIndex--;
            m_playerHealth.GetCurrentHealthCell().MakeCellEmpty();
            return;
        }

        m_healthCellHealEffect.StartHealEffect();
        m_playerHealth.CurrentCellIndex++;
    }
}
