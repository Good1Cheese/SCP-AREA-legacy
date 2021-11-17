using Zenject;

public class PlayerBleedingSound : SoundOnAction
{
    [Inject] private readonly CharacterBleeding _playerBleeding;

    protected override void SubscribeToAction()
    {
        _playerBleeding.OnPlayerBleeding += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _playerBleeding.OnPlayerBleeding -= PlaySound;
    }

}
