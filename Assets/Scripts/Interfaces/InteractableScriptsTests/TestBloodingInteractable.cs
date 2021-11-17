using Zenject;

public class TestBloodingInteractable : IInteractable
{
    [Inject] private readonly CharacterBleeding _playerBleeding;
    public override void Interact()
    {
        _playerBleeding.Bleed();
    }
}
