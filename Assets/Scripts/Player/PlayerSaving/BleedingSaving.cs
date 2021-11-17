using Zenject;

public class BleedingSaving : DataSaving
{
    [Inject] private readonly CharacterBleeding _characterBleeding;

    public bool wasBleeding;

    public override void Save()
    {
        wasBleeding = _characterBleeding.IsBleeding;
    }

    public override void LoadData()
    {
        if (!wasBleeding) { return; }

        _characterBleeding.Bleed();
    }
}