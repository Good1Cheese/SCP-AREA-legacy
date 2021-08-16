public class EmptyDataHandler : DataHandler
{
    void Start()
    {

    }

    public override void LoadData()
    {
        print("Empty Load");
    }

    public override void SaveData()
    {
        print("Empty Save");
    }
}
