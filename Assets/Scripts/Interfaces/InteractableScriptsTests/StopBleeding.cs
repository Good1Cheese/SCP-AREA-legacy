using Zenject;

public class StopBleeding : IInteractable
{
    [Inject] private readonly CharacterBleeding _characterBleeding;

    public override void Interact()
    {
        _characterBleeding.StopBleeding();
    }
}
