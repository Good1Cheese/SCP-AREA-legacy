using Zenject;

public class SlowWalkRunController : RunController
{
    private RunController _runController;

    [Inject]
    private void Construct(RunController runController)
    {
        _runController = runController;
    }

    public override float GetMove()
    {
        if (!_slowWalkController.IsMoving) { return 0; }

        if (_runController.IsMoving)
        {
            UseStarted?.Invoke();
        }

        return Run();
    }
}