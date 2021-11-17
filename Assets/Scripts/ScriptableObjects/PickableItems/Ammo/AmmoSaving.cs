public class AmmoSaving : DataSaving
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
        isAmmoAdded = _ammoHandler.IsAmmoAdded;
        ammoCount = _ammoHandler.AmmoCount;
    }

    public override void LoadData()
    {
        _ammoHandler.IsAmmoAdded = isAmmoAdded;
        _ammoHandler.AmmoCount = ammoCount;
    }
}
