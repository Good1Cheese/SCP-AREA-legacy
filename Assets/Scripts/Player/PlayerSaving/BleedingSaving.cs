using Zenject;

public class BleedingSaving : DataSaving
{
    [Inject] private readonly CharacterBleeding _characterBleeding;

    public bool isBleedingAcitonGoing;

    public override void Save()
    {
        isBleedingAcitonGoing = _characterBleeding.IsActionGoing;
    }

    public override void LoadData()
    {
        if (!isBleedingAcitonGoing) { return; }

        _characterBleeding.StartAction();
    }
}