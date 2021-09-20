using UnityEngine;
using Zenject;

public abstract class DataSaving : MonoBehaviour
{
    [Inject] protected readonly GameSaving m_gameSaving;

    public virtual string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public virtual void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        Load();
    }   

    public abstract void Save();
    public abstract void Load();

}
