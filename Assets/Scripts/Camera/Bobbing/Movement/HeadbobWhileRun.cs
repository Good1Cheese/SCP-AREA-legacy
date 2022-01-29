using Zenject;

public class HeadBobWhileRun : MovementHeadBob
{
    [Inject]
    private void Construct(RunController runController)
    {
        _moveController = runController;
    }
}