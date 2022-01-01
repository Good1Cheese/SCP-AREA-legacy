using Zenject;

public class BleedingStopTest : IInteractable
{
    [Inject] private readonly PlayerBlood _playerBlood;

    public override void Interact()
    {
        _playerBlood.Stop();
    }
}