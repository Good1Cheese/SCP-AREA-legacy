using Zenject;

public class HealthDataSaving : DataSaving
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly InjuryEffectsController m_injuryState;

    public int fullCellsCount;

    public override void Save()
    {
        fullCellsCount = m_playerHealth.HealthCells.GetCurrentCellIndex();
    }

    public override void Load()
    {
        int healthDiffarance = fullCellsCount - m_playerHealth.HealthCells.GetCurrentCellIndex();
        
        if (healthDiffarance > 0)
        {
            for (; healthDiffarance > 0; healthDiffarance--)
            {
                m_playerHealth.HealthCells[m_playerHealth.HealthCells.GetCurrentCellIndex() + healthDiffarance].Fill();
            }
        }
        else
        {
            bool isLastCellHealed = m_playerHealth.HealthCells[m_playerHealth.HealthCells.LastCellIndex].Slider.value == 1;

            if (isLastCellHealed) { healthDiffarance--; }
            if (m_playerHealth.HealthCells.IsCurrentCellLast()) { healthDiffarance++; }

            for (; healthDiffarance < 0; healthDiffarance++)
            {
                m_playerHealth.HealthCells[m_playerHealth.HealthCells.GetCurrentCellIndex()].Clear();
            }
        }
        m_injuryState.ActivateEffects();
    }

}
