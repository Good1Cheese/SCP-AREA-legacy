using UnityEngine;
using Zenject;

public class ItemSaving : GameObjectSaving
{
    [Inject] readonly EmptyDataSaving m_emptyDataHandler;

    int m_saveDataItemIndex;

    public bool isItemInInventory;

    public ItemHandler ItemHandler { get; set; }
    public bool IsSaveable { get; set; } = true;

    protected void Start()
    {
        m_saveDataItemIndex = m_gameSaving.SaveData.IndexOf(this);

        if (m_saveDataItemIndex == -1)
        {
            Debug.LogError("Item not Found " + name);
        }
    }

    public void BecomeSaveable()
    {
        m_gameSaving.SaveData[m_saveDataItemIndex] = this;
    }

    public void BecomeUnsaveable()
    {
        m_gameSaving.SaveData[m_saveDataItemIndex] = m_emptyDataHandler;
    }

    public override void Save()
    {
        isItemInInventory = ItemHandler.IsInInventory;
        base.Save();
    }

    public override void LoadData()
    {
        ItemHandler.SetIsInventoty(isItemInInventory);
        base.LoadData();
    }
}
