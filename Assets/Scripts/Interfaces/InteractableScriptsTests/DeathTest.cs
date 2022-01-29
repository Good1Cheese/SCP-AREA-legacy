using Zenject;

public class DeathTest : Interactable
{
    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public override void Interact()
    {
        _playerHealth.Damage(_playerHealth.Amount);
    }
}