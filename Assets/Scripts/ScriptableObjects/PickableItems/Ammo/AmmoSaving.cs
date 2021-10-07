using UnityEngine;

public class AmmoSaving : DataSaving
{
    AmmoHandler m_ammoHandler;
    public bool isAmmoAdded;
    public int ammoCount;

    void Start()
    {
        m_ammoHandler = GetComponent<AmmoHandler>();
    }

    public override void Save()
    {
        isAmmoAdded = m_ammoHandler.IsAmmoAdded;
        ammoCount = m_ammoHandler.AmmoCount;
    }

    public override void LoadData()
    {
        m_ammoHandler.IsAmmoAdded = isAmmoAdded;
        m_ammoHandler.AmmoCount = ammoCount;
    }
}
