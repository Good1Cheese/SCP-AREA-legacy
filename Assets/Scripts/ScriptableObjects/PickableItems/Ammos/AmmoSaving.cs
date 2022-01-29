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

        ammoCount = _ammoHandler.Ammo;
    }

    public override void Load()
    {
        base.Load();

        _ammoHandler.Ammo = ammoCount;
    }
}