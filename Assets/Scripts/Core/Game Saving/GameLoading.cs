using System;
using System.IO;
using UnityEngine;
using Zenject;

public class GameLoading : MonoBehaviour
{
    private const int EMPTY_JSON_LENGHT = 2;
    [Inject] readonly GameSaving m_gameSaving;
    [Inject] readonly SceneTransition m_sceneTransition;

    public bool WasGameLoadedFromMenu { get; set; } = false;

    public void Load()
    {
        string path = m_gameSaving.GetSaveFilePath();

        if (!File.Exists(path))
        {
            Debug.LogWarning("File not found");
            return;
        }

        PreLoadGame();
    }

    public void PreLoadGame()
    {
        m_gameSaving.SaveData.Clear();
        m_sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.ScpScene);
        WasGameLoadedFromMenu = true;
    }

    public void LoadGame()
    {
        StreamReader reader = new StreamReader(m_gameSaving.GetSaveFilePath());

        string json;
        for (int i = 0; (json = reader.ReadLine()) != null; i++)
        {
            m_gameSaving.SaveData[i].Load(json);
        }

        reader.Close();
    }
}

