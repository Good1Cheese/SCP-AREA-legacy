using UnityEngine;

public class ItemsSaving : DataSaving
{
    private ItemSaveableStateChanger[] _itemDataControllerss;

    public bool[] itemsSaveableStates;

    private void Awake()
    {
        _gameSaving.SaveData.AddRange(gameObject.GetComponentsInChildren<DataSaving>());

        _itemDataControllerss = gameObject.GetComponentsInChildren<ItemSaveableStateChanger>();
        itemsSaveableStates = new bool[_itemDataControllerss.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < _itemDataControllerss.Length; i++)
        {
            itemsSaveableStates[i] = _itemDataControllerss[i].ItemDataHandler.IsSaveable;
        }
    }

    public override void Load()
    {
        for (int i = 0; i < _itemDataControllerss.Length; i++)
        {
            bool isItemSaveable = itemsSaveableStates[i];
            _itemDataControllerss[i].SetSaveableState(isItemSaveable);
            _itemDataControllerss[i].ItemHandler.GameObject.SetActive(isItemSaveable);
        }
    }

    public override void Load(string json)
    {
        ItemSaveableStateChanger[] itemDataHandlers = _itemDataControllerss;
        JsonUtility.FromJsonOverwrite(json, this);
        _itemDataControllerss = itemDataHandlers;
        Load();
    }
}
