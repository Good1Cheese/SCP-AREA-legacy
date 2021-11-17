using Zenject;

public class RunDetector : MoveDetector
{
    [Inject]
    private void Construct(RunController runController)
    {
        _moveController = runController;
    }
}
