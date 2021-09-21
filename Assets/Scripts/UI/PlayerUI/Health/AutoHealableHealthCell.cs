using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthCellHealEffect))]
public class AutoHealableHealthCell : HealthCell
{
    [Inject] readonly GameLoading m_gameLoading;
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly CharacterBleeding m_characterBleeding;

    HealthCellHealEffect m_healthCellHealEffect;
    HealthCells m_healthCells;

    void Start()
    {
        m_healthCellHealEffect = GetComponent<HealthCellHealEffect>();
        m_healthCellHealEffect.Cell = this;
        m_healthCells = m_playerHealth.HealthCells;

        m_playerHealth.OnPlayerHeals += HealCell;
        m_gameLoading.OnGameLoaded += m_healthCellHealEffect.StopHealEffect;
    }

    void HealCell()
    {
        if (!m_healthCells.IsCurrentCellLast(1)) { return; }

        m_healthCellHealEffect.StartHealEffect();
        m_healthCells.GetNextCell().IsFull = true;
    }

    public override void Clear()
    {
        base.Clear();

        if (m_characterBleeding.IsPlayerBleeding) { return; }

        if (m_healthCellHealEffect.IsHealing)
        {
            m_healthCellHealEffect.StopHealEffect();

            m_healthCells.GetFirstFilledCell().Clear();

            return;
        }

        m_healthCellHealEffect.StartHealEffect();
        m_healthCells.GetNextCell().IsFull = true;
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerHeals -= HealCell;
        m_gameLoading.OnGameLoaded -= m_healthCellHealEffect.StopHealEffect;
    }
}
