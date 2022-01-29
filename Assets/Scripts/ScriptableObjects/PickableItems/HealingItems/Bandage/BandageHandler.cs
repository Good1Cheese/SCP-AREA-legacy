using Zenject;

public class BandageHandler : StackableItemHandler
{
    private PlayerBlood _playerBlood;

    [Inject]
    private void Inject(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    public override void Use()
    {
        _playerBlood.StopCoroutine();
    }

    public override bool ShouldItemNotBeUsed => _playerBlood.IsCoroutineGoing;
}