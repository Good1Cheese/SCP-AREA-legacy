using System.Collections.Generic;
using UnityEngine;

public class SaveDataListHandler : DataHandler
{
    public DataHandler[] saveDataArray;

    void Awake()
    {
        m_gameSaving.SaveData.Add(this);
    }

    void Start()
    {
        saveDataArray = new DataHandler[m_gameSaving.SaveData.Count];
    }

    public override void SaveData()
    {
        for (int i = 0; i < m_gameSaving.SaveData.Count; i++)
        {
            saveDataArray[i] = m_gameSaving.SaveData[i];
        }
    }

    public override void LoadData()
    {
        for (int i = 0; i < m_gameSaving.SaveData.Count; i++)
        {
            m_gameSaving.SaveData[i] = saveDataArray[i];
        }
    }

}
