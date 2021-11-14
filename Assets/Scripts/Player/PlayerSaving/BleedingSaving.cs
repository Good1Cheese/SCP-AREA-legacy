using Zenject;

public class BleedingSaving : DataSaving
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public bool wasBleeding;

    public override void Save()
    {
        wasBleeding = m_characterBleeding.IsBleeding;
    }

    public override void LoadData()
    {
        if (!wasBleeding) { return; }

        m_characterBleeding.Bleed();
    }
}