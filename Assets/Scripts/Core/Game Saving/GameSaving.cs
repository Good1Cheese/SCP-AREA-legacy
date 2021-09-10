using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaving : MonoBehaviour
{
    public string FileName { get; set; } = "Player.txt";
    public List<DataHandler> SaveData { get; set; } = new List<DataHandler>();
    public Action OnGameSaving { get; set; }

    public void Save()
    {
        OnGameSaving?.Invoke();

        string path = GetSaveFilePath();
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using StreamWriter writer = new StreamWriter(fileStream);

        WriteSaveData(writer);
        writer.Close();
    }

    void WriteSaveData(StreamWriter writer)
    {
        foreach (var dataHandler in SaveData)
        {
            dataHandler.SaveData();
            writer.WriteLine(dataHandler.ToJson());
        }
    }

    public string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + FileName;
    }

}
