using Zenject;

public class PlayerBleedingSound : SoundOnAction
{
    [Inject] private readonly PlayerBlood _playerBleeding;

    protected override void SubscribeToAction()
    {
        _playerBleeding.Bled += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _playerBleeding.Bled -= PlaySound;
    }

}
