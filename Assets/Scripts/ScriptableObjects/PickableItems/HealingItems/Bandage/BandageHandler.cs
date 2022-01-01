using Zenject;

public class BandageHandler : PickableItemHandler
{
    [Inject] private readonly PlayerBlood _playerBleeding;

    public override void Use()
    {
        _playerBleeding.Stop();
    }

    public override bool ShouldItemNotBeUsed => _playerBleeding.IsCoroutineGoing;
}