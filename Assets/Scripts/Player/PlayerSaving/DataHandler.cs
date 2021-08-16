using UnityEngine;
using Zenject;

public abstract class DataHandler : MonoBehaviour
{
    [Inject] protected readonly GameSaving m_gameSaving;

    void Start()
    {
        m_gameSaving.SaveData.Add(this);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public abstract void SaveData();
    public abstract void LoadData();

}
