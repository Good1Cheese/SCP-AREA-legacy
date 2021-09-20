using UnityEngine;
using Zenject;

public abstract class DataHandler : MonoBehaviour
{
    [Inject] protected readonly GameSaving m_gameSaving;

    public virtual string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public virtual void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        LoadData();
    }   

    public abstract void SaveData();
    public abstract void LoadData();

}
