using UnityEngine;
using Zenject;

public class ItemSaving : GameObjectSaving
{
    [Inject] readonly EmptyDataSaving m_emptyDataHandler;

    int m_saveDataItemIndex;

    public bool isItemInInventory;

    public ItemHandler ItemHandler { get; set; }
    public bool IsSubscribed { get; set; } = true;

    protected void Start()
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

    public override void Save()
    {
        isItemInInventory = ItemHandler.IsInInventory;
        base.Save();
    }

    public override void Load()
    {
        ItemHandler.SetInInventoryState(isItemInInventory);
        base.Load();
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
        Debug.LogError("Item not Found" + name);
        return -1;
    }
}
