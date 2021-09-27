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
        m_characterBleeding.OnPlayerBleedingStarted += m_healthCellHealEffect.StopHealEffect;
        m_characterBleeding.OnPlayerBleedingStopped += HealCell2;
        m_gameLoading.OnGameLoaded += m_healthCellHealEffect.StopHealEffect;
    }

    void HealCell()
    {
        if (!m_healthCells.IsCurrentCellLast(1)) { return; }

        IsFull = true;
        m_healthCellHealEffect.StartHealEffect();
    }

    void HealCell2()
    {
        HealCell();
        if (!m_healthCellHealEffect.IsHealContinueable) { return; }

        IsFull = true;
        m_healthCellHealEffect.StartHealEffect();
    }

    public override void Clear()
    {
        base.Clear();

        if (m_characterBleeding.IsBleeding) { return; }

        if (m_healthCellHealEffect.IsHealing)
        {
            m_healthCellHealEffect.StopHealEffect();

            m_healthCells.GetFirstFilledCell().Clear();

            return;
        }

        m_healthCellHealEffect.StartHealEffect();
        IsFull = true; // Эта строчка ломает сейчас сохранение хп ???
    }

    public override void Fill()
    {
        if (m_healthCellHealEffect.IsHealing)
        {
            m_healthCellHealEffect.StopHealEffect();
        }
        base.Fill();
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerHeals -= HealCell;
        m_characterBleeding.OnPlayerBleedingStarted -= m_healthCellHealEffect.StopHealEffect;
        m_characterBleeding.OnPlayerBleedingStopped -= HealCell2;
        m_gameLoading.OnGameLoaded -= m_healthCellHealEffect.StopHealEffect;
    }
}
