using UnityEngine;
using Zenject;

public class HealthDataSaving : DataHandler
{
    [Inject] readonly PlayerHealth m_playerHealth;

    public int fullCellsCount;

    public override void SaveData()
    {
        fullCellsCount = m_playerHealth.CurrentCellIndex;
    }

    public override void LoadData()
    {
        m_playerHealth.CurrentCellIndex = fullCellsCount;
        for (int i = 0; i < m_playerHealth.Cells.Count; i++)
        {
            if (i > fullCellsCount) { m_playerHealth.Cells[i].MakeCellEmpty(); continue; }
            m_playerHealth.Cells[i].MakeCellFull();
        }
    }

}
