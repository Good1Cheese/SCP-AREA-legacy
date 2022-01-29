using Zenject;

public class PlayerBleedingSound : SoundOnAction
{
    private PlayerBlood _playerBlood;

    [Inject]
    private void Construct(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    protected override void SubscribeToAction()
    {
        _playerBlood.Bled += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _playerBlood.Bled -= PlaySound;
    }
}