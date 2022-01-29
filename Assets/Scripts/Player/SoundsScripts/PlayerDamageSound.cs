using Zenject;

public class PlayerDamageSound : SoundOnAction
{
    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    protected override void SubscribeToAction()
    {
        _playerHealth.Damaged += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _playerHealth.Damaged -= PlaySound;
    }
}