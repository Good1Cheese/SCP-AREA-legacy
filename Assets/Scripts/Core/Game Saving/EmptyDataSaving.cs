public class EmptyDataSaving : DataSaving
{
    public override void Load()
    {
        print("Empty Load");
    }

    public override void Save()
    {
        print("Empty Save");
    }
}
