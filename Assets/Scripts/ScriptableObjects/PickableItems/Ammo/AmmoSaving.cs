public class AmmoSaving : ItemSaving
{
    private AmmoHandler _ammoHandler;
    public bool isAmmoAdded;
    public int ammoCount;

    private void Start()
    {
        _ammoHandler = GetComponent<AmmoHandler>();
    }

    public override void Save()
    {
        base.Save();

        ammoCount = _ammoHandler.AmmoCount;
    }

    public override void LoadData()
    {
        base.LoadData();

        _ammoHandler.AmmoCount = ammoCount;
    }
}
