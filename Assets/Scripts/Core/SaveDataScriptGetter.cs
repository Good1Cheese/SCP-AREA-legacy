using UnityEngine;
using Zenject;

public class SaveDataScriptGetter : MonoBehaviour
{
    private GameSaving _gameSaving;

    [Inject]
    private void Construct(GameSaving gameSaver)
    {
        _gameSaving = gameSaver;
    }

    private void Start()
    {
        var dataSavings = GetComponentsInChildren<DataSaving>();
        _gameSaving.SaveData.AddRange(dataSavings);

        var gameObjectSaving = GetComponentInParent<GameObjectSaving>();
        _gameSaving.SaveData.Add(gameObjectSaving);
    }
}