using System;
using System.IO;
using UnityEngine;
using Zenject;

public class GameLoading : MonoBehaviour
{
    [Inject] readonly GameSaving m_gameSaving;

    public Action OnGameLoaded { get; set; }

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

        for (int i = 0; (json = reader.ReadLine()) != null; i++)
        {
            JsonUtility.FromJsonOverwrite(json, m_gameSaving.SaveData[i]);
            m_gameSaving.SaveData[i].LoadData();
        }
    }

}
