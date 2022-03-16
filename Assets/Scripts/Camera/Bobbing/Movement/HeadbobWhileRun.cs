using Zenject;

public class HeadBobWhileRun : MovementHeadBob
{
    [Inject]
    private void Construct(Run runController)
    {
        _move = runController;
    }
}