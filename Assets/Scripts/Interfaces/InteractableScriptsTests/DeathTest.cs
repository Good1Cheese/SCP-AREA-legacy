using Zenject;

public class DeathTest : IInteractable
{
    [Inject] private readonly PlayerHealth _playerHealth;

    public override void Interact()
    {
        _playerHealth.Damage(_playerHealth.Amount);
    }
}