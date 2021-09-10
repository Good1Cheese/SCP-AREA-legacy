public class PlayerGameObjectDataSaving : GameObjectDataHandler
{
    void Start()
    {
        m_gameSaving.SaveData.Add(this);
    }
}