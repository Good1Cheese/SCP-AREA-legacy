using Zenject;

public class TestDeath : IInteractable
{
    [Inject] private readonly PlayerHealth _playerHealth;

    public override void Interact()
    {
        _playerHealth.Die();
    }
}
