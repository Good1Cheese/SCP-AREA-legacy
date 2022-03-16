using Zenject;

public class WalkSound : MoveSound
{
    [Inject]
    private void Construct(Walk walkController)
    {
        _move = walkController;
    }
}
