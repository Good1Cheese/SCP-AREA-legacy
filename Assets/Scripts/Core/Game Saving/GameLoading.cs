using System;
using System.IO;
using UnityEngine;
using Zenject;

public class GameLoading : MonoBehaviour
{
    [Inject] readonly GameSaving m_gameSaving;
    [Inject] readonly EmptyDataSaving m_emptyDataSaving;

    public Action OnGameLoaded { get; set; }
    public bool WasGameLoadedFromMenu { get; set; } = false;

    public void Load()
    {
        string path = m_gameSaving.GetSaveFilePath();

        if (!File.Exists(path)) { Debug.LogWarning("File not found"); return; }

        using StreamReader reader = new StreamReader(path);
        LoadSavedData(reader);

        reader.Close();
        OnGameLoaded?.Invoke();
    }

    void LoadSavedData(StreamReader reader)
    {
        string json;

        //if (WasGameLoadedFromMenu)
        //{
        //    for (int i = 0; (json = reader.ReadLine()) != null; i++)
        //    {
        //        m_gameSaving.SaveData[i].FromJson(json);
        //    }
        //    WasGameLoadedFromMenu = false;
        //}

        for (int i = 0; (json = reader.ReadLine()) != null; i++)
        {
            if (json.Length <= 2) { continue; }
            JsonUtility.FromJsonOverwrite(json, m_gameSaving.SaveData[i]);
            m_gameSaving.SaveData[i].LoadData();
        }
    }

}
