using Zenject;

public class InventoryDataSaving : DataHandler
{
    [Inject] readonly PlayerInventory m_playerInventory;
    public PickableItem_SO[] m_inventory;


    void Start()
    {
        m_inventory = new PickableItem_SO[m_playerInventory.Inventory.Length];
    }

    public override void SaveData()
    {
        for (int i = 0; i < m_inventory.Length; i++)
        {
            m_inventory[i] = m_playerInventory.Inventory[i];
        }
    }

    public override void LoadData()
    {
        m_playerInventory.CurrentItemIndex = 0;
        for (int i = 0; i < m_inventory.Length; i++)
        {
            m_playerInventory.Inventory[i] = m_inventory[i];
            if (m_inventory[i] != null) { m_playerInventory.CurrentItemIndex = i + 1; }
        }
        m_playerInventory.OnInventoryChanged.Invoke();
    }

    void OnEnable()
    {
        m_gameSaving.SaveData.Add(this);
    }


}
