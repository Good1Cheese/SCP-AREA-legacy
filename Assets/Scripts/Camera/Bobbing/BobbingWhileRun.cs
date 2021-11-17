using Zenject;

internal class BobbingWhileRun : BobbingChangeWhileMoveAction
{
    [Inject]
    private void Construct(RunController runController)
    {
        _moveController = runController;
    }
}
