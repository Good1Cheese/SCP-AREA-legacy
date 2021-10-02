using UnityEngine;
using Zenject;

public class SaveDataScriptGetter : MonoBehaviour
{
    [Inject] readonly GameSaving m_gameSaver;

    void Start()
    {
        DataSaving[] collection = GetComponentsInChildren<DataSaving>();
        m_gameSaver.SaveData.AddRange(collection);
        m_gameSaver.SaveData.Add(GetComponentInParent<GameObjectSaving>());
    }
}
