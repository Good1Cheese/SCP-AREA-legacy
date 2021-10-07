using UnityEngine;

public class ItemsSaving : DataSaving
{
    ItemDataController[] m_itemDataControllerss;

    public bool[] itemsSaveableStates;

    void Awake()
    {
        m_gameSaving.SaveData.AddRange(gameObject.GetComponentsInChildren<DataSaving>());

        m_itemDataControllerss = gameObject.GetComponentsInChildren<ItemDataController>();
        itemsSaveableStates = new bool[m_itemDataControllerss.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < m_itemDataControllerss.Length; i++)
        {
            itemsSaveableStates[i] = m_itemDataControllerss[i].ItemDataHandler.IsSubscribed;
        }
    }

    public override void LoadData()
    {
        for (int i = 0; i < m_itemDataControllerss.Length; i++)
        {
            bool isItemSaveable = itemsSaveableStates[i];
            m_itemDataControllerss[i].SetSavableState(isItemSaveable);
            m_itemDataControllerss[i].ItemHandler.GameObject.SetActive(isItemSaveable);
        }
    }

    public override void Load(string json)
    {
        ItemDataController[] itemDataHandlers = m_itemDataControllerss;
        JsonUtility.FromJsonOverwrite(json, this);
        m_itemDataControllerss = itemDataHandlers;
        LoadData();
    }
}
