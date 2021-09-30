using Zenject;

public class HealthSaving : DataSaving
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly InjuryEffectsController m_injuryState;

    public bool[] cellsFullStates;

    void Start()
    {
        cellsFullStates = new bool[m_playerHealth.HealthCells.Cells.Count];
    }

    public override void Save()
    {
        for (int i = 0; i < m_playerHealth.HealthCells.Cells.Count; i++)
        {
            cellsFullStates[i] = m_playerHealth.HealthCells.Cells[i].IsFull;
        }
    }

    public override void Load()
    {
        for (int i = 0; i < m_playerHealth.HealthCells.Cells.Count; i++)
        {
            bool isCellFull = cellsFullStates[i];

            if (isCellFull == m_playerHealth.HealthCells[i].IsFull) { continue; }

            if (isCellFull)
            {
                m_playerHealth.HealthCells[i].Fill();
                continue;
            }
            m_playerHealth.HealthCells[i].Clear();

        }

        m_injuryState.ActivateEffects();

    }

}
