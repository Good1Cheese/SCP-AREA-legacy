using Zenject;

public class SlowWalkRunController : RunController
{
    [Inject] readonly private RunController _runController;

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