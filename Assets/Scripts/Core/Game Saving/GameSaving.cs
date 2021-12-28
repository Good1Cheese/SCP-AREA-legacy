using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSaving : MonoBehaviour
{
    [SerializeField] private string _fileName;

    public List<DataSaving> SaveData { get; set; } = new List<DataSaving>();

    public void Save()
    {
        string path = GetSaveFilePath();
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using StreamWriter writer = new StreamWriter(fileStream);

        WriteSaveData(writer);
        writer.Close();
    }

    private void WriteSaveData(StreamWriter writer)
    {
        foreach (DataSaving dataHandler in SaveData)
        {
            dataHandler.Save();
            writer.WriteLine(dataHandler.ToJson());
        }
    }

    public string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + _fileName;
    }
}
