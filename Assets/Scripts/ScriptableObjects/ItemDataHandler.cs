using UnityEngine;
using Zenject;

public class ItemDataHandler : GameObjectDataHandler
{
    [Inject] readonly GameSaving m_gameSaver;
    [Inject] readonly EmptyDataHandler m_emptyDataHandler;

    int m_saveDataItemIndex;
    public bool IsSubscribed { get; set; } = true;

    void Start()
    {
        m_gameSaver.SaveData.Add(this);
        m_saveDataItemIndex = FindItemIndex();
    }

    public void BecomeSaveable()
    {
        IsSubscribed = true;
        m_gameSaver.SaveData[m_saveDataItemIndex] = this;
    }

    public void BecomeUnsaveable()
    {
        IsSubscribed = false;
        m_gameSaver.SaveData[m_saveDataItemIndex] = m_emptyDataHandler;
    }

    int FindItemIndex()
    {
        for (int i = 0; i < m_gameSaver.SaveData.Count; i++)
        {
            if (this == m_gameSaver.SaveData[i])
            {
                return i;
            }
        }
        Debug.LogError("Item not Found");
        return -1;
    }

}
