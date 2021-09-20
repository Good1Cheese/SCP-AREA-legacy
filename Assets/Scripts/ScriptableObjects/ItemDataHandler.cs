using UnityEngine;
using Zenject;

public class ItemDataHandler : GameObjectDataSaving
{
    [Inject] readonly EmptyDataSaving m_emptyDataHandler;

    int m_saveDataItemIndex;

    public bool IsSubscribed { get; set; } = true;

    void Start()
    {
        m_saveDataItemIndex = FindItemIndex();
    }

    public void BecomeSaveable()
    {
        IsSubscribed = true;
        m_gameSaving.SaveData[m_saveDataItemIndex] = this;
    }

    public void BecomeUnsaveable()
    {
        IsSubscribed = false;
        m_gameSaving.SaveData[m_saveDataItemIndex] = m_emptyDataHandler;
    }

    int FindItemIndex()
    {
        for (int i = 0; i < m_gameSaving.SaveData.Count; i++)
        {
            if (this == m_gameSaving.SaveData[i])
            {
                return i;
            }
        }
        Debug.LogError("Item not Found");
        return -1;
    }

}
