using System.IO;
using UnityEngine;
using Zenject;

public class GameLoading : MonoBehaviour
{
    [Inject] private readonly GameSaving _gameSaving;
    [Inject] private readonly SceneTransition _sceneTransition;

    public bool WasGameLoadedFromMenu { get; set; }

    public void Load()
    {
        string path = _gameSaving.GetSaveFilePath();

        if (!File.Exists(path))
        {
            Debug.LogWarning("File not found");
            return;
        }

        PreLoadGame();
    }

    public void PreLoadGame(bool value = true)
    {
        _gameSaving.SaveData.Clear();
        _sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.ScpScene);
        WasGameLoadedFromMenu = value;
    }

    public void LoadGame()
    {
        StreamReader reader = new StreamReader(_gameSaving.GetSaveFilePath());

        string json;
        for (int i = 0; (json = reader.ReadLine()) != null; i++)
        {
            _gameSaving.SaveData[i].Load(json);
        }

        reader.Close();
    }
}