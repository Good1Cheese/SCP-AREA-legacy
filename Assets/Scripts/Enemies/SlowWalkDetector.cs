using Zenject;

public class SlowWalkDetector : MoveDetector
{
    [Inject]
    private void Construct(SlowWalkController slowWalkController)
    {
        _moveController = slowWalkController;
    }
}
