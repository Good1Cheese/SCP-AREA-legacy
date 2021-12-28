using Zenject;

public class WalkSound : MoveSound
{
    [Inject]
    private void Construct(WalkController walkController)
    {
        _moveController = walkController;
    }
}
