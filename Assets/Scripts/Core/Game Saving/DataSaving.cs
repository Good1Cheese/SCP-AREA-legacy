using UnityEngine;
using Zenject;

public abstract class DataSaving : MonoBehaviour
{
    [Inject] protected readonly GameSaving m_gameSaving;

    public abstract void Save();
    public abstract void Load();

    public virtual void LoadFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        Load();
    }

    public virtual string ToJson() => JsonUtility.ToJson(this);

}
