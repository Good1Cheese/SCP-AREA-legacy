using UnityEngine;

public class ItemsSaving : DataSaving
{
    private ItemSaveableStateChanger[] _itemDataControllers;

    public bool[] itemsSaveableStates;

    private void Awake()
    {
        _gameSaving.SaveData.AddRange(gameObject.GetComponentsInChildren<DataSaving>());

        _itemDataControllers = gameObject.GetComponentsInChildren<ItemSaveableStateChanger>();
        itemsSaveableStates = new bool[_itemDataControllers.Length];
    }

    public override void Save()
    {
        for (int i = 0; i < _itemDataControllers.Length; i++)
        {
            itemsSaveableStates[i] = _itemDataControllers[i].ItemDataHandler.IsSaveable;
        }
    }

    public override void Load()
    {
        for (int i = 0; i < _itemDataControllers.Length; i++)
        {
            bool isItemSaveable = itemsSaveableStates[i];
            _itemDataControllers[i].SetSaveableState(isItemSaveable);
            _itemDataControllers[i].ItemHandler.GameObject.SetActive(isItemSaveable);
        }
    }

    public override void Load(string json)
    {
        ItemSaveableStateChanger[] itemDataHandlers = _itemDataControllers;
        JsonUtility.FromJsonOverwrite(json, this);
        _itemDataControllers = itemDataHandlers;
        Load();
    }
}
