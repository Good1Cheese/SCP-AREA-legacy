using Zenject;

public class WalkDetector : MoveDetector
{
    [Inject]
    private void Construct(WalkController walkController)
    {
        _moveController = walkController;
    }
}
