using UnityEngine;
using Zenject;

public class ItemsDataSaving : DataSaving
{
    [Inject] readonly GameSaving m_gameSaving;

    ItemDataController[] m_itemDataHandler;

    public bool[] itemsSaveableStates;

    void Awake()
    {
        m_gameSaving.SaveData.Add(this);
        m_itemDataHandler = gameObject.GetComponentsInChildren<ItemDataController>();
        itemsSaveableStates = new bool[m_itemDataHandler.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < m_itemDataHandler.Length; i++)
        {
            itemsSaveableStates[i] = m_itemDataHandler[i].ItemDataHandler.IsSubscribed;
        }
    }

    public override void Load()
    {
        for (int i = 0; i < m_itemDataHandler.Length; i++)
        {
            bool isItemSaveable = itemsSaveableStates[i];
            m_itemDataHandler[i].SetSavableState(isItemSaveable);
            m_itemDataHandler[i].ItemHandler.GameObject.SetActive(isItemSaveable);
        }
    }

}
