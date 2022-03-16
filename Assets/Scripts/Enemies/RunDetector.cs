using Zenject;

public class RunDetector : MoveDetector
{
    [Inject]
    private void Construct(Run runController)
    {
        _move = runController;
    }
}
