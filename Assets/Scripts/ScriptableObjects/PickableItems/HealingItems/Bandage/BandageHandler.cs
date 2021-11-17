using Zenject;

public class BandageHandler : PickableItemHandler
{
    [Inject] private readonly CharacterBleeding _playerBleeding;

    public override void Use()
    {
        _playerBleeding.StopBleeding();
    }

    public override bool ShouldItemNotBeUsed => !_playerBleeding.IsBleeding;
}
