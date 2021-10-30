public class InjectableItemSaving : ItemSaving
{
    public int numOfUses;

    InjectableItemHandler InjectableItemHandler => (InjectableItemHandler)ItemHandler;

    public override void Save()
    {
        numOfUses = InjectableItemHandler.NumsOfUses;
        base.Save();
    }

    public override void LoadData()
    {
        InjectableItemHandler.SetNumOfUses(numOfUses);
        base.LoadData();
    }
}