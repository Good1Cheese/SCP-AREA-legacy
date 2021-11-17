using UnityEngine;
using Zenject;

public class SaveDataScriptGetter : MonoBehaviour
{
    [Inject] private readonly GameSaving _gameSaver;

    private void Start()
    {
        DataSaving[] collection = GetComponentsInChildren<DataSaving>();
        _gameSaver.SaveData.AddRange(collection);
        _gameSaver.SaveData.Add(GetComponentInParent<GameObjectSaving>());
    }
}
