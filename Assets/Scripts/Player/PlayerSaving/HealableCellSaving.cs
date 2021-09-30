using UnityEngine;
using Zenject;

public class HealableCellSaving : DataSaving
{
    [Inject] readonly PlayerHealth m_playerHealth;

    AutoHealableHealthCell m_autoHealableHealthCell;

    public float healProgress;
    public bool ishealContinueable;

    void Start()
    {
        m_autoHealableHealthCell = m_playerHealth.HealthCells.GetFirstFilledCell() as AutoHealableHealthCell;
    }

    public override void Save()
    {
        ishealContinueable = m_autoHealableHealthCell.HealthCellHealEffect.IsHealContinueable;
        healProgress = m_autoHealableHealthCell.Slider.value;
    }

    public override void Load()
    {
        if (m_playerHealth.HealthCells.IsCurrentCellLast() && ishealContinueable)
        {
            m_autoHealableHealthCell.Slider.value = healProgress;
            m_autoHealableHealthCell.HealthCellHealEffect.StopHealEffect();
            m_autoHealableHealthCell.HealthCellHealEffect.StartHealEffect();
        }
    }



}
