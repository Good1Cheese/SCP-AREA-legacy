using Zenject;

internal class BobbingWhileSlowWalk : BobbingChangeWhileMoveAction
{
    [Inject]
    private void Construct(SlowWalkController slowWalkController)
    {
        _moveController = slowWalkController;
    }
}