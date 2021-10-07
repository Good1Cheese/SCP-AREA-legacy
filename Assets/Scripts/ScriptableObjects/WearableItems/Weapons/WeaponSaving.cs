public class WeaponSaving : ItemSaving
{
    WeaponHandler m_weaponHandler;

    public SilencerHandler silencerHandler;
    public int ammoCount;
    public int clipAmmo;
    public bool isAmmoAdded;

    new void Start()
    {
        base.Start();
        m_weaponHandler = GetComponent<WeaponHandler>();
    }

    public override void Save()
    {
        silencerHandler = m_weaponHandler.SilencerHandler;
        ammoCount = m_weaponHandler.AmmoCount;
        clipAmmo = m_weaponHandler.ClipAmmo;
        base.Save();
    }

    public override void LoadData()
    {
        base.LoadData();

        m_weaponHandler.AmmoCount = ammoCount;
        m_weaponHandler.ClipAmmo = clipAmmo;

        if (silencerHandler == null && m_weaponHandler.SilencerHandler != null)
        {
            m_weaponHandler.SilencerHandler.Unequip();
        }
    }
}