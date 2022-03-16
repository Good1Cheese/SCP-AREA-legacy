using Zenject;

public class WalkDetector : MoveDetector
{
    [Inject]
    private void Construct(Walk walkController)
    {
        _move = walkController;
    }
}
