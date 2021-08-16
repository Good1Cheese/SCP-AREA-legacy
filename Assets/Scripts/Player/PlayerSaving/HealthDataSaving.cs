using UnityEngine;
using Zenject;

public class HealthDataSaving : DataHandler
{
    [Inject] readonly PlayerHealth m_playerHealth;

    public int fullCellsCount;

    public override void SaveData()
    {
        fullCellsCount = m_playerHealth.CurrentHealthCellIndex;
    }

    public override void LoadData()
    {
        m_playerHealth.CurrentHealthCellIndex = fullCellsCount;
        for (int i = 0; i < m_playerHealth.HealthCells.Count; i++)
        {
            if (i > fullCellsCount) { m_playerHealth.HealthCells[i].MakeCellEmpty(); continue; }
            m_playerHealth.HealthCells[i].MakeCellFull();
        }
    }

}
