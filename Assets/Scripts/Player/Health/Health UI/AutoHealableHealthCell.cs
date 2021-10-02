using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthCellHealEffect))]
public class AutoHealableHealthCell : HealthCell
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly CharacterBleeding m_characterBleeding;

    HealthCells m_healthCells;

    public HealthCellHealEffect HealthCellHealEffect { get; set; }

    void Start()
    {
        HealthCellHealEffect = GetComponent<HealthCellHealEffect>();
        HealthCellHealEffect.Cell = this;
        m_healthCells = m_playerHealth.HealthCells;

        m_playerHealth.OnPlayerHeals += HealCell;
        m_characterBleeding.OnPlayerBleedingStarted += HealthCellHealEffect.StopHealEffect;
        m_characterBleeding.OnPlayerBleedingStopped += HealCellOnBleedingStopped;
    }

    void HealCell()
    {
        if (!m_healthCells.IsCurrentCellLast(1)) { return; }

        IsFull = true;
        HealthCellHealEffect.StartHealEffect();
    }

    void HealCellOnBleedingStopped()
    {
        HealCell();
        if (!HealthCellHealEffect.IsHealContinueable) { return; }

        IsFull = true;
        HealthCellHealEffect.StartHealEffect();
    }

    public override void Clear()
    {
        base.Clear();

        if (m_characterBleeding.IsBleeding || !m_playerHealth.HealthCells.IsCurrentCellLast(1)) { return; }

        if (HealthCellHealEffect.IsHealing)
        {
            HealthCellHealEffect.StopHealEffect();

            m_healthCells.GetFirstFilledCell().Clear();

            return;
        }

        HealthCellHealEffect.StartHealEffect();
        if (!m_playerHealth.HealthCells.IsCurrentCellLast(1)) { return; }
        IsFull = true;
    }

    public override void Fill()
    {
        if (HealthCellHealEffect.IsHealing)
        {
            HealthCellHealEffect.StopHealEffect();
        }
        base.Fill();
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerHeals -= HealCell;
        m_characterBleeding.OnPlayerBleedingStarted -= HealthCellHealEffect.StopHealEffect;
        m_characterBleeding.OnPlayerBleedingStopped -= HealCellOnBleedingStopped;
    }
}
