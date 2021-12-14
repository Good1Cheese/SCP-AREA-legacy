using Zenject;

public class PlayerDamageSound : SoundOnAction
{
    [Inject] private readonly PlayerHealth _playerHealth;

    protected override void SubscribeToAction()
    {
        _playerHealth.GetsNonBleedDamage += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _playerHealth.GetsNonBleedDamage -= PlaySound;
    }
}
