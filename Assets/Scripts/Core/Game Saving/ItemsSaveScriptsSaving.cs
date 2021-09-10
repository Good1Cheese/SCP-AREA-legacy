using Zenject;

public class ItemsSaveScriptsSaving : DataHandler
{
    [Inject] readonly EmptyDataSaving m_emptyDataSaving;
    public DataHandler[] saveDataArray;

    void Start()
    {
        saveDataArray = new DataHandler[m_gameSaving.SaveData.Count];
    }

    public override void SaveData()
    {
    }

    public override void LoadData()
    {
        for (int i = 0; i < m_gameSaving.SaveData.Count; i++)
        {
            if (m_gameSaving.SaveData[i] != m_emptyDataSaving) { return; }
            m_gameSaving.SaveData[i] = saveDataArray[i];
        }
    }

}
